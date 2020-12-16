using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplePlayerData : MonoBehaviour
{

    [SerializeField]
    private PlayerData data = null;

    public void setPlayerData(PlayerData d)
    {
        data = d;
    }

    public PlayerData getPlayerData()
    {
        return data;
    }
}
