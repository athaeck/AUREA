using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject arSessionOrigin;
    public GameObject arSession;
    public bool arOn = true;

    public void SwitchMode() {
        arOn = !arOn;

        mainCamera.SetActive(!arOn);
        arSessionOrigin.SetActive(arOn);
        arSession.SetActive(arOn);
    }
}
