using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
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
    private List<PlayerAureaData> playerAureaData = new List<PlayerAureaData>();

    [SerializeField]
    private List<string> squad = new List<string>();

    [SerializeField]
    private List<PlayerItemData> items = new List<PlayerItemData>();

    public List<string> GetSquad() { return squad; }
    public void AddAurea(PlayerAureaData _aurea)
    {
        playerAureaData.Add(_aurea);
    }
    public void AddAureaToSquad(string _aurea) { squad.Add(_aurea); }
    public int GetAureaLevel(string name)
    {
        foreach(PlayerAureaData data in playerAureaData)
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
    public int GetCorwns() { return crowns; }
    public int GetMoney() { return money; }
}
