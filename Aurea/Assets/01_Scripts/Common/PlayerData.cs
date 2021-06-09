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
    public int won = 0;

    [SerializeField]
    public int lose = 0;

    [SerializeField]
    public int draw = 0;

    [SerializeField]
    public int crowns = 0;

    [SerializeField]
    public int money = 0;

    [SerializeField]
    public bool ar = true;

    [SerializeField]
    public List<PlayerAureaData> playerAureaData = new List<PlayerAureaData>();

    [SerializeField]
    public List<string> squad = new List<string>();

    [SerializeField]
    public List<PlayerItemData> items = new List<PlayerItemData>();

    public Difficulty difficulty;

    [SerializeField]
    public bool animationsOn = true;

    [SerializeField]
    public float backgroundVol = 0;

    [SerializeField]
    public float sfxVol = 0;

    [SerializeField]
    public float distance = 0;

    [SerializeField]
    public float size = 0;
    #endregion


    #region Functions
    public bool AnimationsOn() { return animationsOn; }
    public void SetAnimations(bool _anim) { animationsOn = _anim; }
    public List<string> GetSquad() { return squad; }
    public void SetSquad(List<string> _squad) { squad = _squad; }
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

    public void BuyItem(int amount, PlayerItemData item)
    {

        money -= amount;
        items.Add(item);
    }

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
    public void AddItem(PlayerItemData item)
    {
        items.Add(item);
    }
    public List<PlayerItemData> GetItems()
    {
        return items;
    }
    public void setVol(float bgVol, float sfxVol)
    {
        backgroundVol = bgVol;
        this.sfxVol = sfxVol;
    }

    public void setArSlider(float disSlider, float sizeSlider)
    {
        distance = disSlider;
        size = sizeSlider;
        StateManager.SavePlayer(Player.Instance);
    }

    public void setName(string name)
    {
        NAME = name;
    }
    #endregion
}
