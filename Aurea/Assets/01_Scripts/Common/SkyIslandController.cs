using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyIslandController : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPlace = null;

    [SerializeField]
    private GameObject character = null;

    [SerializeField]
    private Camera cam = null;


    private void Start()
    {
        if(spawnPlace != null && character != null && cam != null)
        {
            Init();
        }
    }


    public void ResetIsland() {
        Init();
        Debug.Log("Reset SkyISland");
    }

    private void Init()
    {
        character.transform.position = spawnPlace.transform.position;

        cam.transform.position = spawnPlace.transform.position;

        character.GetComponent<MovementController>().Move(character.transform.position);
    }
}
