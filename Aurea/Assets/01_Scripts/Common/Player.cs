using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class Player : MonoBehaviour
{
    #region Singelton
    [SerializeField]
    private static PlayerData _instance;

    [SerializeField]
    public static PlayerData Instance
    {
        get
        {
            if (_instance == null)
                _instance = StateManager.LoadPlayer();
            return _instance;
        }
    }
    #endregion

    public string GameSceneName = "Game";

    public static void RealodPlayerData()
    {
        _instance = StateManager.LoadPlayer();
    }

    void Start()
    {
        Debug.Log(Player.Instance.NAME);
        DontDestroyOnLoad(this);
        SceneManager.LoadSceneAsync(GameSceneName);
    }
}
