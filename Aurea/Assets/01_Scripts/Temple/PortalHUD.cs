using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHUD : MonoBehaviour
{
    
    public void Teleport()
    {
        IslandController.Instance.OpenSkyIsland();
    }

    public void close()
    {
        gameObject.SetActive(false);
    }
}
