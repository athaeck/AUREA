using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetController : MonoBehaviour
{
    private SkyIslandController _skyIslandController = null;

    private TempleController _templeController = null;
    private void Start()
    {
        _skyIslandController = IslandController.Instance.skyIsland;
        _templeController = IslandController.Instance.temple;
    }
    public void RespwanPlayer()
    {
        switch (IslandController.Instance.activeIsland)
        {
            case Island.SkyIsland:
                _skyIslandController.ResetPlayerPosition();
                break;
            case Island.TempleOfDoom:
                _templeController.ResetPlayerPosition();
                break;
            default:
                break;
        }
    }
}
