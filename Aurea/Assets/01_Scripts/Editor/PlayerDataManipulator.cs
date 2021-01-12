using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDataManipulator : EditorWindow
{
    PlayerData playerData = null;


    [MenuItem("Window/PlayerData Manipulator")]

    public static void ShowWindow()
    {
        GetWindow<PlayerDataManipulator>("Player Data Manipulator");
    }

    void OnGUI()
    {
        if (playerData == null)
            LoadData();

        GUILayout.Label("Player Data", EditorStyles.boldLabel);
        playerData.NAME = EditorGUILayout.TextField("Name", playerData.NAME);
        playerData.won = EditorGUILayout.IntField("Won", playerData.won);
        playerData.lose = EditorGUILayout.IntField("Lose", playerData.lose);
        playerData.crowns = EditorGUILayout.IntField("Crowns", playerData.crowns);
        playerData.money = EditorGUILayout.IntField("Money", playerData.money);
        playerData.ar = EditorGUILayout.Toggle("AR on", playerData.ar);
        playerData.difficulty = (Difficulty)EditorGUILayout.EnumPopup("Schwierigkeit", playerData.difficulty);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Space(20);
        GUILayout.Label("Player Aurea", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        foreach (PlayerAureaData aurea in playerData.playerAureaData)
        {
            aurea.aureaName = EditorGUILayout.TextField(aurea.aureaName);
            EditorGUILayout.BeginHorizontal();
            aurea.aureaLevel = EditorGUILayout.IntField(aurea.aureaLevel);

            if (GUILayout.Button("Löschen"))
            {
                playerData.playerAureaData.Remove(aurea);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Aurea hinzufügen"))
        {
            PlayerAureaData newData = new PlayerAureaData();
            playerData.playerAureaData.Add(newData);
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Space(20);
        GUILayout.Label("Aurea in squad", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        for (int i = 0; i < playerData.squad.Count; i++)
        {

            EditorGUILayout.BeginHorizontal();
            playerData.squad[i] = EditorGUILayout.TextField(playerData.squad[i]);

            if (GUILayout.Button("Lösche " + playerData.squad[i]))
            {
                playerData.squad.Remove(playerData.squad[i]);
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
            //EditorGUILayout.BeginVertical();

        }

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Aurea in squad hinzufügen"))
        {
            if (playerData.squad.Count < 5)
            {
                string newData = "";
                playerData.squad.Add(newData);
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.Space(20);
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Neu Laden"))
        {
            LoadData();
        }

        if (GUILayout.Button("Speichern"))
        {
            SaveData();
        }
    }

    private void LoadData()
    {
        playerData = StateManager.LoadPlayer();
    }

    private void SaveData()
    {
        StateManager.SavePlayer(playerData);
    }
}
