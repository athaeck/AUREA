using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class StateManager
{
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        return CreateNewPlayerData();
    }

    public static void SavePlayer(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData CreateNewPlayerData()
    {
        PlayerData newData = new PlayerData();
        newData.NAME = "Player";

        for(int i = 0; i < 5; i++)
        {
            PlayerAureaData newAurea = new PlayerAureaData();
            newAurea.aureaName = "Golem";
            newAurea.aureaLevel = 2;
            newData.AddAurea(newAurea);
            newData.AddAureaToSquad("Golem");
        }
        return newData;
    }
}
