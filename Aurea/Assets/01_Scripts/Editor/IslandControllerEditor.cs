using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(IslandController))]
public class IslandControllerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        IslandController myScript = (IslandController)target;
        if(GUILayout.Button("Activate SkyIsland"))
        {
            myScript.ChangeActiveIsland(Island.SkyIsland);
        }

        if(GUILayout.Button("Activate Temple"))
        {
            myScript.ChangeActiveIsland(Island.TempleOfDoom);
        }

        if(GUILayout.Button("Activate Fight"))
        {
            myScript.ChangeActiveIsland(Island.ChickenFight);
        }
    }
}