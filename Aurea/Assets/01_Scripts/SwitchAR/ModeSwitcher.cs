using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject arSessionOrigin;
    public GameObject arSession;
    public GameObject loadingScreen;
    public float waitForSeconds = 1.5f;

    private void Start()
    {
        StartCoroutine(Starting());
    }

    public void SwitchMode()
    {
        Debug.Log("SwitchMode");
        Player._instance.SwitchARMode();
        InitView(Player._instance.IsArOn());
    }

    public void InitView(bool arOn) {
        Debug.Log("Im Here");
        mainCamera.SetActive(!arOn);
        arSessionOrigin.SetActive(arOn);
        arSession.SetActive(arOn);
    }

    IEnumerator Starting() {
        loadingScreen.SetActive(true);
        InitView(true);
        yield return new WaitForSecondsRealtime(waitForSeconds);
        InitView(Player._instance.IsArOn());
        loadingScreen.SetActive(false);
    }
}
