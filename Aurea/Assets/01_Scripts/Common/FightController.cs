using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class FightController : MonoBehaviour
{
    
    public Action GameStarting;
    public Action GameLoaded;
    public Action GameEnded;
    public Action ResetFight;
    public Action ResettedSelections;
    public Action StartedUsingSkill;
    public Action EndedUsingSkill;
    public Action<Aurea> SelectedAurea;
    public Action<Aurea> TargetedAurea;
    public Action<PlayerController> TurnChanged;
    public Action<PlayerController> LoadedPlayer;
    public Action<PlayerController> LoadedEnemy;

    [SerializeField]
    public AureaList aureaData = null;

    [SerializeField]
    private PlayerController player = null;

    [SerializeField]
    private PlayerController enemy = null;

    [SerializeField]
    private List<GameObject> playerSpawnpoints = new List<GameObject>();

    [SerializeField]
    private List<GameObject> enemySpawnpoints = new List<GameObject>();

    [SerializeField]
    private float roundTime = 30f;

    private PlayerController activePlayer = null;
    private bool canInteract = true;
    private float timeLeft = 0;
    private bool timerStarted = false;

    public void TakeInput(Ray ray) {
        Debug.Log("Got input: Fight");
    }
    public void ResetIsland() {
        Debug.Log("Reset Fight");
        ResetFight?.Invoke();
        StartGame();
    }
    
    private void Update()
    {
        if(!timerStarted) { return; }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
            EndTurn();
    }

    void StartGame()
    {
        GameStarting?.Invoke();

        // Load PlayerData
        // player.SetData(StateManager.LoadPlayer());

        //Load Enemy Data


        LoadedPlayer?.Invoke(player);
        player.StartGame(playerSpawnpoints);

        LoadedEnemy?.Invoke(player);
        enemy.StartGame(enemySpawnpoints);

        player.GameOver += EndGame;
        enemy.GameOver += EndGame;
        enemy.AureaHasDied += EnemyDied;

        int startPlayerNumber = FlipCoin();

        startPlayerNumber = 0;

        StartTurn(startPlayerNumber % 2 == 0 ? player : enemy);
        GameLoaded?.Invoke();
    }

    int FlipCoin()
    {
        return Random.Range(0, 2);
    }

    public void EnemyDied(Aurea _aurea)
    {
        player.ResetSelectedTarget();
    }

    void EndGame(PlayerController _player)
    {
        canInteract = false;

        PlayerData data = player.GetData();

        if(player == _player)
        {
            enemy.Won();
            data.AddLoseStatistics();
        }
        else
        {
            player.Won();
            data.AddWonStatistics();
        }

        player.SetData(data);
        StateManager.SavePlayer(data);

        GameEnded?.Invoke();
    }

    void StartTurn(PlayerController player)
    {
        activePlayer = player;

        player.StartTurn();

        timerStarted = true;
        timeLeft = roundTime;

        TurnChanged?.Invoke(player);
    }

    public void StartUsingSkill()
    {
        canInteract = false;
        StartedUsingSkill?.Invoke();
    }

    public void EndUsingSkill()
    {
        canInteract = true;
        EndedUsingSkill?.Invoke();
    }
    public void EndTurn()
    {
        activePlayer.Select(null);
        activePlayer.EndTurn();

        timerStarted = false;
        NextTurn();
    }

    void NextTurn()
    {
        StartTurn(activePlayer == player ? enemy : player);
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public PlayerController GetPlayer()
    {
        return player;
    }

    public PlayerController GetEnemy() { return enemy; }

    public AureaData GetAureaData(string name)
    {
        foreach(AureaData data in aureaData.aureas)
        {
            if (data.NAME == name)
                return data;
        }

        Debug.Log("Didnt found: " + name);
        return null;
    }
}
