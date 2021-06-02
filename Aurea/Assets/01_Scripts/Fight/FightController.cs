using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using Random = UnityEngine.Random;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class FightController : MonoBehaviourPunCallbacks
{
    public static FightController instance;
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

    // public NetworkedVar<PlayerController> p;

    [Header("Machine Learning")]
    [SerializeField] public bool training = false;

    [Header("Game Stuff")]

    [SerializeField]
    public AureaList aureaData = null;

    [SerializeField]
    public FightCameraController camController = null;

    [SerializeField]
    public List<PlayerController> players = new List<PlayerController>();

    // [SerializeField]
    // private PlayerController oldplayer = null;
    // [SerializeField]
    // private RandomPlayerController randomPlayer = null;

    // [SerializeField]
    // private PlayerController olsenemy = null;
    // [SerializeField]
    // private AgentController enemyAgent = null;

    [SerializeField]
    private List<GameObject> playerSpawnpoints = new List<GameObject>();
    [SerializeField]
    private List<GameObject> playerCrystals = new List<GameObject>();
    [SerializeField]
    private List<Transform> playerCamPositions = new List<Transform>();

    [SerializeField]
    private List<GameObject> enemySpawnpoints = new List<GameObject>();
    [SerializeField]
    private List<GameObject> enemyCrystals = new List<GameObject>();
    [SerializeField]
    private List<Transform> enemyCamPositions = new List<Transform>();
    [SerializeField]
    private GameObject playAgainButton = null;
    [SerializeField]
    private GameObject returnButton = null;

    [SerializeField]
    public float roundTime = 30f;

    [SerializeField]
    private float waitBetweenClicks = 1.5f;

    [SerializeField]
    private GameObject gameOverScreen = null;

    [SerializeField]
    private Text gameOverText = null;

    [SerializeField]
    private SelectSkillController skillController = null;

    [SerializeField]
    private FightCameraController cameraController = null;

    [SerializeField]
    private TimeVisualizationController timeController = null;

    [SerializeField]
    public PhotonView view = null;

    public PlayerController activePlayer = null;
    private bool canInteract = true;
    public float timeLeft = 0;
    private bool timerStarted = false;
    bool justClicked = false;
    bool justSelectedSkill = false;
    bool gameEnded = false;
    public bool firstStart = true;

    private void Awake()
    {
        if (FightController.instance)
            Debug.LogError("Already an FightController Instnace");

        FightController.instance = this;
    }

    public void ResetIsland()
    {
        gameEnded = false;
        ResetFight?.Invoke();
    }

    public void ResetToIsland(string _island)
    {
        // gameEnded = false;
        // ResetFight?.Invoke();
        // foreach (PlayerController player in players)
        // {
        //     player.ResetAureaInstances();
        //     PhotonView.Destroy(player.gameObject);
        // }
        // NetworkController.instance.Kill();
        SceneManager.LoadSceneAsync(_island, LoadSceneMode.Single);
    }

    // public void ReturnToSkyIsland() 
    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && timeController && !timeController.timerStarted) return;
        if (gameEnded) { return; }

        // timeLeft -= Time.deltaTime;

        if (timeController && timeController.timeLeft <= 0f)
            EndTurn();
    }

    void StartGame()
    {
        GameStarting?.Invoke();

        gameOverScreen.SetActive(false);
        foreach (PlayerController player in players)
        {
            if (player.view.Owner.IsLocal)
            {
                // player.GameOver += EndGame;
                player.StartGame();
            }
        }

        if(PhotonNetwork.IsMasterClient) {
            GameObject timeControllerObject = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "TimeCanvas"), transform.position, Quaternion.identity);
            timeController = timeControllerObject.GetComponent<TimeVisualizationController>();
            view.RPC("AddTimeController", RpcTarget.AllBuffered, timeController.view.ViewID);
            timeController.StartTimer();
        }

        PlayerController masterPlayer = players[0].view.Owner.IsMasterClient ? players[0] : players[1];
        StartTurn(masterPlayer);

        canInteract = true;
        GameLoaded?.Invoke();
    }

    [PunRPC]
    public void AddTimeController(int _viewID) {
        timeController = PhotonView.Find(_viewID).gameObject.GetComponent<TimeVisualizationController>();
    }

    public void Register(PlayerController _player)
    {
        if (_player.view.Owner.IsMasterClient)
        {
            _player.GetComponent<CrystalVisualizationController>()?.TakeCrystals(playerCrystals);
            _player.TakeSpawnPoints(playerSpawnpoints);
            if (_player.view.Owner.IsLocal)
            {
                camController.TakePositions(playerCamPositions);
                skillController.TakePlayer(_player);
            }
        }
        else
        {
            _player.GetComponent<CrystalVisualizationController>()?.TakeCrystals(enemyCrystals);
            _player.TakeSpawnPoints(enemySpawnpoints);
            if (_player.view.Owner.IsLocal)
            {
                camController.TakePositions(enemyCamPositions);
                skillController.TakePlayer(_player);
            }
        }
        players.Add(_player);

        _player.GameOver += GameOver;

        if (players.Count > 1)
            StartGame();
    }

    public void TakeInput(Ray _ray)
    {
        if (justClicked || gameEnded) { return; }

        if (Physics.Raycast(_ray, out RaycastHit hit, float.MaxValue))
        {
            EvaluateInput(hit);
        }
    }

    public void TakeInput(Touch _touch)
    {
        Ray ray = CameraController.Instance.activeCamera.ScreenPointToRay(_touch.position);

        TakeInput(ray);
    }

    public void JustSelectedSkill()
    {
        StartCoroutine(WaitBetweenClickSelectedSkill());
    }

    private void EvaluateInput(RaycastHit _hit)
    {
        if (justClicked || justSelectedSkill || gameEnded) return;

        if (EventSystem.current.IsPointerOverGameObject())
        {
            StartCoroutine(WaitBetweenClick());
            return;
        }

        Aurea hero = null;

        if (_hit.collider.CompareTag("Aurea"))
            hero = _hit.collider.GetComponent<Aurea>();

        if (hero)
            activePlayer.view.RPC("Select", RpcTarget.AllBuffered, hero.view.ViewID);
        else
            activePlayer.view.RPC("Select", RpcTarget.AllBuffered, -1);

        if (_hit.collider.CompareTag("EndTurn") && activePlayer.view.Owner.IsLocal && activePlayer.IsOnTurn())
            activePlayer.view.RPC("ManuallyEndTurn", RpcTarget.AllBuffered);

        StartCoroutine(WaitBetweenClick());
    }

    public void ClearSlots()
    {
        foreach (GameObject slot in playerSpawnpoints)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (GameObject slot in enemySpawnpoints)
        {
            foreach (Transform child in slot.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void LoadData()
    {
        // oldplayer.SetData(Player.Instance);
        // LoadedPlayer?.Invoke(oldplayer);

        // LoadedEnemy?.Invoke(oldplayer);
    }

    int FlipCoin()
    {
        return Random.Range(0, 2);
    }

    public void EnemyDied(Aurea _aurea)
    {
        // player.ResetSelectedTarget();
    }

    void GameOver(PlayerController _player)
    {
        view.RPC("EndGame", RpcTarget.AllBuffered, _player.view.ViewID);
    }

    [PunRPC]
    void EndGame(int _playerID)
    {
        Debug.Log("EndGame!!!!!!!!!!!!!!!!!!");
        canInteract = false;
        gameEnded = true;

        PlayerController winPlayer = players[1].view.ViewID == _playerID ? players[0] : players[1];
        PlayerController lostPlayer = players[0].view.ViewID == _playerID ? players[0] : players[1];

        // PlayerController losePlayer = PhotonView.Find(_playerID).GetComponent<PlayerController>();
        // NetworkController.instance.Kill();
        StartCoroutine(CloseIn(3f));

        winPlayer.Won();

        if (lostPlayer.view.Owner.IsLocal)
        {
            gameOverText.text = "You lose!";
            // SceneManager.LoadSceneAsync("Lose");
        }
        else
        {
            gameOverText.text = "You won!";
            // SceneManager.LoadSceneAsync("Won");
        }



        gameOverScreen.SetActive(true);

        // PlayerData data = oldplayer.GetData();
        // PlayerData enemyData = olsenemy.GetData();

        // if (oldplayer == _player)
        // {
        //     gameOverText.text = "You lose!";
        //     olsenemy.Won();
        //     data.AddLoseStatistics();
        //     enemyData.AddWonStatistics();
        // }
        // else
        // {
        //     gameOverText.text = "You won!";
        //     oldplayer.Won();
        //     data.AddWonStatistics();
        //     enemyData.AddLoseStatistics();
        // }

        // oldplayer.SetData(data);
        // olsenemy.SetData(enemyData);

        // if (!training)
        //     StateManager.SavePlayer(data);

        // if (!training)
        //     gameOverScreen.SetActive(true);

        // GameEnded?.Invoke();

        // if (training)
        //     ResetIsland();
    }

    [PunRPC]
    void StartTurn(PlayerController _player)
    {
        activePlayer = _player;

        _player.view.RPC("StartTurn", RpcTarget.AllBuffered);

        timerStarted = true;
        timeLeft = roundTime;

        TurnChanged?.Invoke(_player);
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
        activePlayer.view.RPC("EndTurn", RpcTarget.AllBuffered);

        // timerStarted = false;
        // timeController.EndTimer();
        if(PhotonNetwork.IsMasterClient)
            timeController.EndTimer();
        NextTurn();
    }

    void NextTurn()
    {
        Debug.Log("Start new Turn");
        StartTurn(activePlayer == players[0] ? players[1] : players[0]);

        if(PhotonNetwork.IsMasterClient)
            timeController.StartTimer();
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    public PlayerController GetPlayer()
    {
        return players[0];
    }

    public PlayerController GetEnemy() { return players[0]; }

    public AureaData GetAureaData(string _name)
    {
        foreach (AureaData data in aureaData.aureas)
        {
            if (data.NAME == _name)
                return data;
        }

        return null;
    }

    IEnumerator CloseIn(float _time)
    {
        playAgainButton.SetActive(false);
        returnButton.SetActive(false);
        
        yield return new WaitForSeconds(_time);

        if (NetworkController.instance)
            NetworkController.instance.Kill();

        playAgainButton.SetActive(true);
        returnButton.SetActive(true);
    }
    IEnumerator WaitBetweenClick()
    {
        justClicked = true;
        yield return new WaitForSeconds(waitBetweenClicks);
        justClicked = false;
    }

    IEnumerator WaitBetweenClickSelectedSkill()
    {
        justSelectedSkill = true;
        yield return new WaitForSeconds(waitBetweenClicks);
        justSelectedSkill = false;
    }
}
