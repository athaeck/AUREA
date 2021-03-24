using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    // TO-DO Hier kommt die ULTI hin

    [SerializeField]
    private List<Aurea> aureaInstances = new List<Aurea>();

    [SerializeField]
    private int actionPointsLeft = 0;

    [SerializeField]
    private int actionPointsPerRound = 3;

    private Aurea selected = null;

    void Start()
    {
        IslandController.Instance.fight.ResetFight += ResetPlayer;
    }

    public void StartGame(List<GameObject> spawnPoints)
    {
        if (IslandController.Instance.fight.training)
            CreateRandomSquad();

        InstantiateSquad(spawnPoints);

        AddAP(actionPoints);
    }
    public void CreateRandomSquad()
    {
        List<string> newsquad = new List<string>();
        while (newsquad.Count != 3)
        {
            int rnd = UnityEngine.Random.Range(0, 3);
            if(newsquad.Contains(data.playerAureaData[rnd].aureaName))
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

    void InstantiateSquad(List<GameObject> spawnPoints)
    {
        int i = 0;
        List<string> squad = data.GetSquad();
        foreach (GameObject spawnPoint in spawnPoints)
        {
            int aureaLevel = data.GetAureaLevel(squad[i]);
            AureaData aureaData = IslandController.Instance.fight.GetAureaData(squad[i]);
            GameObject aureaPrefab = aureaData.levels[aureaLevel - 1].prefab;
            Aurea aurea = Instantiate(aureaPrefab, spawnPoint.transform).GetComponent<Aurea>();
            aurea.transform.localPosition = new Vector3(0, aureaData.instantiateAtheight, 0);
            aurea.Init(aureaLevel, this);
            aureaInstances.Add(aurea);
            aurea.Died += AureaDied;


            i++;
        }
    }
    public void StartTurn()
    {
        StartedTurn?.Invoke();
        isOnTurn = true;
        AddAP(actionPointsPerRound);
    }

    public void EndTurn()
    {
        EndedTurn?.Invoke();
        isOnTurn = false;

        if (selected)
            selected.CancelSkill();

        ResetedSelection?.Invoke();
    }

    public void ManuallyEndTurn()
    {
        // Debug.Log("Manually end Turn on " + gameObject.name + " is " + !isOnTurn + " and " + !IslandController.Instance.fight.CanInteract());
        if (!isOnTurn || !IslandController.Instance.fight.CanInteract())
            return;

        IslandController.Instance.fight.EndTurn();
    }

    public void AureaDied(Aurea _aurea)
    {
        AureaHasDied?.Invoke(_aurea);
        foreach (Aurea aurea in aureaInstances)
        {
            if (aurea.IsAlive())
                return;
        }
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

    public void Select(Aurea _aurea)
    {
        if (_aurea && !_aurea.IsAlive())
            return;

        if (!_aurea)
        {
            if (selected)
                selected.CancelSkill();

            AbortedSkill?.Invoke();
            return;
        }

        if (selected)
        {
            // Debug.Log("Selected aurea: " + _aurea.name);
            selected.TakeTarget(_aurea);
        }
        else if (_aurea.GetPlayer() == this)
        {
            selected = _aurea;
            selected.SkillCancled += RemoveSelected;
            SelectedAurea?.Invoke(selected);
        }
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
    public void ResetAureaInstances() { aureaInstances = new List<Aurea>(); }
    public List<Aurea> GetAureas() { return aureaInstances; }
    public bool IsOnTurn() { return isOnTurn; }
    public void SetData(PlayerData _data) { data = _data; }
    public PlayerData GetData() { return data; }
}
