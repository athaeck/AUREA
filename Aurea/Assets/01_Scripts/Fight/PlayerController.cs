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
    private Aurea target = null;
    private Skill selectedSkill = null;

    void Start()
    {
        IslandController.Instance.fight.ResetFight += ResetPlayer;
    }

    public void StartGame(List<GameObject> spawnPoints)
    {
        InstantiateSquad(spawnPoints);

        AddAP(actionPoints);
    }

    void ResetPlayer()
    {
        Debug.Log("To DO Reeset Player");
    }

    void InstantiateSquad(List<GameObject> spawnPoints)
    {
        int i = 0;
        List<string> squad = data.GetSquad();
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (squad[i] == "" || squad[i] == null)
            {
                Debug.Log("More Spawnpoints than Aureas in Squad");
                break;
            }

            int aureaLevel = data.GetAureaLevel(squad[i]);
            GameObject aureaPrefab = IslandController.Instance.fight.GetAureaData(squad[i]).levels[aureaLevel - 1].prefab;
            Aurea aurea = Instantiate(aureaPrefab, spawnPoint.transform).GetComponent<Aurea>();
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
    }

    public void ManuallyEndTurn()
    {
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
        Debug.Log("Won!");
        foreach (Aurea aurea in aureaInstances)
        {
            if (aurea.IsAlive()) //{
                HasWon?.Invoke();
            // aurea.GetComponent<Animator>().SetTrigger("Victory");
            // }
        }
    }

    public void Select(Aurea _aurea)
    {
        if (!_aurea)
        {
            RemoveSelected();
            ResetedSelection?.Invoke();
            return;
        }

        if (selected)
        {
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

        Debug.Log("Remove Selected Aurea");

        selected.SkillCancled -= RemoveSelected;
        selected = null;
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
