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

    bool toolkitOpen = false;

    private void Start()
    {
        StartCoroutine(Starting());
    }

    public void ToggleToolkit()
    {
        toolkitOpen = !toolkitOpen;
        toolkitContent.SetActive(toolkitOpen);
    }

    public void ChangeSize()
    {
        if (Player._instance.IsArOn())
        {
            Vector3 newSize = new Vector3(sizeSlider.value, sizeSlider.value, sizeSlider.value) * initARScaling;
            islands.transform.localScale = newSize;
        }
    }

    public void ChangeDistance()
    {
        if (Player._instance.IsArOn())
            placementController.distance = distanceSlider.value;
    }

    public void SwitchMode()
    {
        Player._instance.SwitchARMode();
        if (Player._instance.IsArOn())
        {
            islands.transform.localScale.Scale(Vector3.one * initARScaling);
            ChangeDistance();
            ChangeSize();
            InitView(true);
        }
        else
        {
            islands.transform.localScale = Vector3.one;
            islands.transform.position = Vector3.zero;
            InitView(false);
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
        InitView(true);
        yield return new WaitForSecondsRealtime(waitForSeconds);
        InitView(Player._instance.IsArOn());
        islands.transform.position = Vector3.zero;
        loadingScreen.SetActive(false);
    }
}
