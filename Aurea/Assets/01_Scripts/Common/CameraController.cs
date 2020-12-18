using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singelton
    private static CameraController _instance = null;
    public static CameraController Instance
    {
        get
        {
            if (!_instance)
                Debug.LogError("No Instance found");
            return _instance;
        }
    }
    #endregion
    [SerializeField]
    private Camera skyIslandCamera = null;

    [SerializeField]
    private Camera templeCamera = null;

    [SerializeField]
    private Camera fightCamera = null;

    [SerializeField]
    private Camera arCamera = null;

    [SerializeField]
    private GameObject arSessionOrigin = null;

    [SerializeField]
    private GameObject arSession = null;

    public Camera activeCamera {
        get; private set;
    }

    private void Awake()
    {
        if (!_instance)
            _instance = this;
        else
            Debug.Log("More than one CameraController Instances");
    }

    public void ChangeIsland(Island _island, bool arOn)
    {
        skyIslandCamera.gameObject.SetActive(false);
        templeCamera.gameObject.SetActive(false);
        fightCamera.gameObject.SetActive(false);
        arSession.SetActive(arOn);
        arSessionOrigin.SetActive(arOn);

        if (arOn) {
            activeCamera = arCamera;
            return;
        }

        switch (_island)
        {
            case Island.SkyIsland:
                skyIslandCamera.gameObject.SetActive(true);
                activeCamera = skyIslandCamera;
                break;
            case Island.TempleOfDoom:
                templeCamera.gameObject.SetActive(true);
                activeCamera = templeCamera;
                break;
            case Island.ChickenFight:
                fightCamera.gameObject.SetActive(true);
                activeCamera = fightCamera;
                break;
        }
    }
}
