using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARToolkitController : MonoBehaviour
{
    [SerializeField]
    private GameObject toolkitContent = null;

    [SerializeField]
    private GameObject islands = null;

    [SerializeField]
    private Slider sizeSlider = null;

    [SerializeField]
    private Slider distanceSlider = null;

    [SerializeField]
    private GameObject mainCamera = null;

    [SerializeField]
    private GameObject arSessionOrigin = null;

    [SerializeField]
    private GameObject arSession = null;

    [SerializeField]
    private PlacementController placementController = null;

    [SerializeField]
    private GameObject loadingScreen = null;

    [SerializeField]
    private float waitForSeconds = 1.5f;

    [SerializeField]
    private float initARScaling = 0.01f;

    [SerializeField]
    private List<GameObject> objectsToTurnOff = new List<GameObject>();

    bool toolkitOpen = false;

    private void Start()
    {
        Init();
        // SwitchMode();
        // SwitchMode();
        //StartCoroutine(Starting());
    }

    public void ToggleToolkit()
    {
        toolkitOpen = !toolkitOpen;
        toolkitContent.SetActive(toolkitOpen);
    }

    public void ChangeSize()
    {
        if (Player.Instance.IsArOn())
        {
            Vector3 newSize = new Vector3(sizeSlider.value, sizeSlider.value, sizeSlider.value) * initARScaling;
            islands.transform.localScale = newSize;
        }
    }

    public void ChangeDistance()
    {
        if (Player.Instance.IsArOn())
            placementController.distance = distanceSlider.value;
    }

    public void SwitchMode()
    {
        Player.Instance.SwitchARMode();
        Init();
    }
    public void Init()
    {
        if (Player.Instance.IsArOn())
        {
            islands.transform.localScale.Scale(Vector3.one * initARScaling);
            ChangeDistance();
            ChangeSize();
            CameraController.Instance.ChangeIsland(IslandController.Instance.activeIsland, true);
            SetObjectsActive(false);
            IslandController.Instance.skyIsland.ResetPlayerPosition();
            IslandController.Instance.temple.ResetIsland();
        }
        else
        {
            islands.transform.localScale = Vector3.one;
            islands.transform.position = Vector3.zero;
            CameraController.Instance.ChangeIsland(IslandController.Instance.activeIsland, false);
            SetObjectsActive(true);
            IslandController.Instance.skyIsland.ResetPlayerPosition();
            IslandController.Instance.temple.ResetIsland();
        }
    }

    void SetObjectsActive(bool _active)
    {
        foreach (GameObject obj in objectsToTurnOff)
        {
            obj.SetActive(_active);
        }
    }

    public void InitView(bool _arOn)
    {
        mainCamera.SetActive(!_arOn);
        arSessionOrigin.SetActive(_arOn);
        arSession.SetActive(_arOn);
    }

    IEnumerator Starting()
    {
        loadingScreen.SetActive(true);
        CameraController.Instance.ChangeIsland(IslandController.Instance.activeIsland, true);
        // InitView(true);
        yield return new WaitForSecondsRealtime(waitForSeconds);
        CameraController.Instance.ChangeIsland(IslandController.Instance.activeIsland, Player.Instance.IsArOn());
        // InitView(Player.Instance.IsArOn());
        islands.transform.position = Vector3.zero;
        loadingScreen.SetActive(false);
    }
}
