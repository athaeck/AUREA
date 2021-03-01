using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    public FightCameraController camController = null;

    [SerializeField]
    private PlayerController player = null;

    [SerializeField]
    private PlayerController enemy = null;

    [SerializeField]
    private List<GameObject> playerSpawnpoints = new List<GameObject>();

    [SerializeField]
    private List<GameObject> enemySpawnpoints = new List<GameObject>();

    [SerializeField]
    public float roundTime = 30f;

    [SerializeField]
    private float waitBetweenClicks = 1.5f;

    [SerializeField]
    private GameObject gameOverScreen = null;

    [SerializeField]
    private Text gameOverText = null;

    public PlayerController activePlayer = null;
    private bool canInteract = true;
    public float timeLeft = 0;
    private bool timerStarted = false;
    bool justClicked = false;
    bool justSelectedSkill = false;
    bool gameEnded = false;

    public void TakeInput(Ray ray)
    {
        if (justClicked || !player.IsOnTurn()) { return; }

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            EvaluateInput(hit);
        }
    }

    public void TakeInput(Touch touch)
    {
        Ray ray = CameraController.Instance.activeCamera.ScreenPointToRay(touch.position);

        TakeInput(ray);
    }

    public void JustSelectedSkill()
    {
        StartCoroutine(WaitBetweetClickSelectedSkill());
    }

    private void EvaluateInput(RaycastHit hit)
    {
        Debug.Log("Evaluate Input");
        if (justClicked || justSelectedSkill) return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(WaitBetweetClick());
            return;
        }

        Aurea hero = null;

        if (hit.collider.CompareTag("Aurea"))
            hero = hit.collider.GetComponent<Aurea>();

        activePlayer.Select(hero);

        if (hit.collider.CompareTag("EndTurn") && player.IsOnTurn())
            player.ManuallyEndTurn();


        StartCoroutine(WaitBetweetClick());
    }

    public void ResetIsland()
    {
        ClearSlots();
        LoadData();
        gameEnded = false;
        ResetFight?.Invoke();
        StartGame();
    }

    public void ClearSlots()
    {
        foreach(GameObject slot in playerSpawnpoints) {
            foreach(Transform child in slot.transform) {
                DestroyImmediate(child.gameObject);
            }   
        }
        foreach(GameObject slot in enemySpawnpoints) {
            foreach(Transform child in slot.transform) {
                DestroyImmediate(child.gameObject);
            }   
        }
    }

    private void Update()
    {
        if (!timerStarted || gameEnded) { return; }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
            EndTurn();
    }

    void StartGame()
    {
        GameStarting?.Invoke();

        gameOverScreen.SetActive(false);

        LoadData();

        player.StartGame(playerSpawnpoints);
        enemy.StartGame(enemySpawnpoints);

        player.GameOver += EndGame;
        enemy.GameOver += EndGame;
        enemy.AureaHasDied += EnemyDied;

        int startPlayerNumber = FlipCoin();

        startPlayerNumber = 0;

        StartTurn(startPlayerNumber % 2 == 0 ? player : enemy);
        GameLoaded?.Invoke();
    }

    void LoadData()
    {
        // Load PlayerData
        player.SetData(Player.Instance);
        LoadedPlayer?.Invoke(player);

        //Load Enemy Data
        //Its HardCoded right now
        LoadedEnemy?.Invoke(player);
    }

    int FlipCoin()
    {
        return Random.Range(0, 2);
    }

    public void EnemyDied(Aurea _aurea)
    {
        // player.ResetSelectedTarget();
    }

    void EndGame(PlayerController _player)
    {
        canInteract = false;
        gameEnded = true;

        PlayerData data = player.GetData();

        if (player == _player)
        {
            gameOverText.text = "You lose!";
            enemy.Won();
            data.AddLoseStatistics();
        }
        else
        {
            gameOverText.text = "You won!";
            player.Won();
            data.AddWonStatistics();
        }

        player.SetData(data);
        StateManager.SavePlayer(data);

        gameOverScreen.SetActive(true);

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
        activePlayer.EndTurn();

        timerStarted = false;
        NextTurn();
    }

    void NextTurn()
    {
        Debug.Log("Start new Turn");
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
        foreach (AureaData data in aureaData.aureas)
        {
            if (data.NAME == name)
                return data;
        }

        Debug.Log("Didnt found: " + name);
        return null;
    }
    IEnumerator WaitBetweetClick()
    {
        justClicked = true;
        yield return new WaitForSeconds(waitBetweenClicks);
        justClicked = false;
    }

    IEnumerator WaitBetweetClickSelectedSkill()
    {
        justSelectedSkill = true;
        yield return new WaitForSeconds(waitBetweenClicks);
        justSelectedSkill = false;
    }
}
