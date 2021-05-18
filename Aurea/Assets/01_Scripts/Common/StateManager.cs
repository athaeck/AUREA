using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class StateManager
{
    static string path = "/player.data";
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + StateManager.path;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }

        Debug.Log("Load - LOADED PLAYER COMPLETED");
        return CreateNewPlayerData();
    }

    public static void SavePlayer(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + StateManager.path;
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();

        Player.RealodPlayerData();
        Debug.Log("Save - COMPLETED");
    }

    public static PlayerData CreateNewPlayerData()
    {
        PlayerData newData = new PlayerData();
        newData.NAME = "Player";

        PlayerAureaData golem = new PlayerAureaData();
        golem.aureaName = "Golem";
        golem.aureaLevel = 1;
        newData.AddAurea(golem);
        newData.AddAureaToSquad("Golem");

        PlayerAureaData inkubus = new PlayerAureaData();
        inkubus.aureaName = "Inkubus";
        inkubus.aureaLevel = 1;
        newData.AddAurea(inkubus);
        newData.AddAureaToSquad("Inkubus");

        PlayerAureaData crystal = new PlayerAureaData();
        crystal.aureaName = "Crystal";
        crystal.aureaLevel = 1;
        newData.AddAurea(crystal);
        newData.AddAureaToSquad("Crystal");


        SavePlayer(newData);

        Debug.Log(Application.persistentDataPath + StateManager.path);
        return newData;
    }

    public static bool DeletePlayerData()
    {
        //TODO
        Debug.Log("Delete - DELETION OF PLAYER DATA COMPLETED");
        return false;
    }
}
