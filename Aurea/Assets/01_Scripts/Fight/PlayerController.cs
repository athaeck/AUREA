using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkedVar;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public Action StartedTurn;
    public Action EndedTurn;
    public Action<int> ChangedAP;
    public Action<Aurea> SelectedAurea;
    public Action<Aurea> SelectedTarget;
    public Action ResetTarget;
    public Action ResetedSelection;
    public Action AbortedSkill;
    public Action<PlayerController> GameOver;
    public Action<Aurea> AureaHasDied;
    public Action HasWon;

    [SerializeField]
    public bool isPlayer = false;

    [SerializeField]
    private PlayerData data = null;


    [SerializeField]
    private int actionPoints = 15;

    [SerializeField]
    private bool isOnTurn = false;

    [SerializeField]
    private List<Aurea> aureaInstances = new List<Aurea>();

    [SerializeField]
    private int actionPointsLeft = 0;

    [SerializeField]
    private int actionPointsPerRound = 3;
    [SerializeField]
    public List<GameObject> spawnPoints = new List<GameObject>();

    public PhotonView view = null;
    private Aurea selected = null;

    private bool registered = false;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {

        if (FightController.instance && !registered)
        {
            if (view != null && view.Owner.IsLocal)
            {
                view.RPC("RegisterAtFightController", RpcTarget.AllBuffered);
            }

        }
    }

    [PunRPC]
    public void RegisterAtFightController()
    {
        FightController.instance.Register(this);
        registered = true;
        isPlayer = view.Owner.IsLocal;
    }
    public void TakeSpawnPoints(List<GameObject> _spawnPoints) => spawnPoints = _spawnPoints;

    [PunRPC]
    public void StartGame()
    {
        if (view.Owner.IsLocal)
        {
            IslandController.Instance.fight.ResetFight += ResetPlayer;
            SetData(Player.Instance);

            InstantiateSquad();

            AddAP(actionPoints);
        }

        // foreach(Aurea aurea in aureaInstances) {
        //     Debug.Log("Installed Listener");
        //     aurea.Died += AureaDied;
        // }
    }

    public void CreateRandomSquad()
    {
        List<string> newsquad = new List<string>();
        while (newsquad.Count != 3)
        {
            int rnd = UnityEngine.Random.Range(0, 3);
            if (newsquad.Contains(data.playerAureaData[rnd].aureaName))
                continue;
            newsquad.Add(data.playerAureaData[rnd].aureaName);
        }
        data.SetSquad(newsquad);
    }

    void ResetPlayer()
    {
        foreach (Aurea aurea in aureaInstances)
        {
            if (aurea != null)
                DestroyImmediate(aurea.gameObject);
        }
        aureaInstances = new List<Aurea>();
    }

    public void InstantiateSquad()
    {
        int i = 0;
        List<string> squad = data.GetSquad();
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int aureaLevel = data.GetAureaLevel(squad[i]);

            AureaData aureaData = IslandController.Instance.fight.GetAureaData(squad[i]);
            GameObject aureaPrefab = aureaData.levels[aureaLevel - 1].prefab;
            GameObject aureaObject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", aureaPrefab.name), spawnPoint.transform.position, spawnPoint.transform.rotation);
            Aurea aurea = aureaObject.GetComponent<Aurea>();

            aureaObject.transform.SetParent(spawnPoint.transform);
            aurea.transform.localPosition = new Vector3(0, aureaData.instantiateAtheight, 0);

            // aurea.Init(aureaLevel, this);
            aurea.view.RPC("Init", RpcTarget.AllBuffered, aureaLevel, this.view.ViewID);
            view.RPC("AddAurea", RpcTarget.AllBuffered, aurea.view.ViewID);

            i++;
        }
    }

    [PunRPC]
    public void AddAurea(int _viewID)
    {
        Aurea aurea = PhotonView.Find(_viewID).gameObject.GetComponent<Aurea>();
        aureaInstances.Add(aurea);
    }

    [PunRPC]
    public void StartTurn()
    {
        StartedTurn?.Invoke();
        isOnTurn = true;
        AddAP(actionPointsPerRound);
    }

    [PunRPC]
    public void EndTurn()
    {
        EndedTurn?.Invoke();
        isOnTurn = false;

        if (selected)
            selected.CancelSkill();

        ResetedSelection?.Invoke();
    }

    [PunRPC]
    public void ManuallyEndTurn()
    {
        // Debug.Log("Manually end Turn on " + gameObject.name + " is " + !isOnTurn + " and " + !IslandController.Instance.fight.CanInteract());
        if (!isOnTurn || !IslandController.Instance.fight.CanInteract())
            return;

        IslandController.Instance.fight.EndTurn();
    }

    public void AureaDied(Aurea _aurea)
    {
        Debug.Log("Aurea Died!");
        AureaHasDied?.Invoke(_aurea);
        foreach (Aurea aurea in aureaInstances)
        {
            if (aurea.IsAlive())
                return;
        }
        Debug.Log("GameOver!");
        GameOver?.Invoke(this);
    }

    public void Won()
    {
        // Debug.Log("Won!");
        foreach (Aurea aurea in aureaInstances)
        {
            if (aurea.IsAlive())
                HasWon?.Invoke();
        }
    }

    public void Print(string _val)
    {
        Debug.Log(_val);
    }
    [PunRPC]
    public void Select(int _viewID)
    {
        if (_viewID == -1)
        {
            Select(null);
            Debug.Log("Selected Null");
            return;

        }
        Aurea aurea = PhotonView.Find(_viewID).gameObject.GetComponent<Aurea>();

        if (aurea)
            Debug.Log("Found Aurea by ID with name: " + aurea.name);
        else
            Debug.Log("Didnt found Aurea by ID");

        Select(aurea);
    }

    public void Select(Aurea _aurea)
    {
        Debug.Log("Aurea before selected");


        if (_aurea && !_aurea.IsAlive())
            return;

        Debug.Log("Aurea and aurea alive");

        if (!_aurea)
        {
            if (selected)
                selected.CancelSkill();

            Debug.Log("Aborted Skill");

            AbortedSkill?.Invoke();
            return;
        }

        Debug.Log("Selected: " + (selected ? selected.name : "null"));

        if (selected)
        {
            Debug.Log("Selected Target: " + _aurea.name);
            selected.TakeTarget(_aurea);
        }
        else if (_aurea.GetPlayer() == this)
        {
            Debug.Log("Aurea selected");
            selected = _aurea;
            selected.SkillCancled += RemoveSelected;
            SelectedAurea?.Invoke(selected);
        }
    }

    [ServerRPC]
    public void SelectServerRpc()
    {
        Debug.Log("Got a ServerRPC");
    }

    [ClientRPC]
    public void SelectClientRpc()
    {
        Debug.Log("Got a ClientRPC");
    }

    public void RemoveSelected()
    {
        if (!selected) return;


        selected.SkillCancled -= RemoveSelected;
        // selected.CancelSkill();

        selected = null;
        // ResetedSelection?.Invoke();
    }
    void AddAP(int amount)
    {
        actionPointsLeft = Mathf.Clamp(actionPointsLeft + amount, 0, actionPoints);
        ChangedAP?.Invoke(actionPointsLeft);
    }

    public void RemoveAP(int amount)
    {
        actionPointsLeft = Mathf.Clamp(actionPointsLeft - amount, 0, actionPoints);
        ChangedAP?.Invoke(actionPointsLeft);
    }

    public int HeroesLeft()
    {
        int heros = 0;
        foreach (Aurea hero in aureaInstances)
        {
            heros += hero.IsAlive() ? 1 : 0;
        }
        return heros;
    }

    bool IsOwnAurea(Aurea aureaToCheck)
    {
        foreach (Aurea aurea in aureaInstances)
        {
            if (aurea == aureaToCheck)
                return true;
        }

        return false;
    }
    public int GetAPLeft() { return actionPointsLeft; }
    public void ResetAureaInstances()
    {
        foreach (Aurea aurea in aureaInstances)
        {
            PhotonView.Destroy(aurea.gameObject);
        }
        aureaInstances = new List<Aurea>();
    }
    public List<Aurea> GetAureas() { return aureaInstances; }
    public bool IsOnTurn() { return isOnTurn; }
    public void SetData(PlayerData _data) { data = _data; }
    public PlayerData GetData() { return data; }
}
