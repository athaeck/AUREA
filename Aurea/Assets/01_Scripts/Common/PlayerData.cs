using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    #region Stats
    [SerializeField]
    public string NAME = "";

    [SerializeField]
    private int won = 0;

    [SerializeField]
    private int lose = 0;

    [SerializeField]
    private int draw = 0;

    [SerializeField]
    private int crowns = 0;

    [SerializeField]
    private int money = 0;

    [SerializeField]
    private bool ar = true;

    [SerializeField]
    private List<PlayerAureaData> playerAureaData = new List<PlayerAureaData>();

    [SerializeField]
    private List<string> squad = new List<string>();

    [SerializeField]
    private List<PlayerItemData> items = new List<PlayerItemData>();

    private Difficulty difficulty;
    #endregion


    #region Functions

    public List<string> GetSquad() { return squad; }
    public List<PlayerAureaData> GetAurea() { return playerAureaData; }
    public void AddAurea(PlayerAureaData _aurea)
    {
        playerAureaData.Add(_aurea);
    }
    public void AddAureaToSquad(string _aurea) { squad.Add(_aurea); }
    public void RemoveAureaToSquad(string _aurea) { squad.Remove(_aurea); }
    public int GetAureaLevel(string name)
    {
        foreach (PlayerAureaData data in playerAureaData)
        {
            if (data.aureaName == name)
                return data.aureaLevel;
        }
        return -1;
    }
    public int GetWonStatistics() { return won; }
    public void AddWonStatistics() { won++; }
    public int GetLoseStatistics() { return lose; }
    public void AddLoseStatistics() { lose++; }
    public int GetDrawStatistics() { return draw; }
    public void AddDrawStatistics() { draw++; }
    public int GetCrowns() { return crowns; }
    public void AddCrowns(int amount) { crowns += amount; }
    public int GetMoney() { return money; }
    public void AddMoney(int amount) { money += amount; }

    public void SetDifficulty(Difficulty df)
    {
        difficulty = df;
    }
    public Difficulty GetDifficulty()
    {
        return difficulty;
    }
    public bool IsArOn() { return ar; }
    public void SwitchARMode()
    {
        ar = !ar;
        StateManager.SavePlayer(this);
    }
    #endregion
}
