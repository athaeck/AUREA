using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour
{

    [SerializeField]
    private GameObject buttons = null;
    [SerializeField]
    private ARToolkitController arToolkit = null;
    [SerializeField]
    private GameObject arButton = null;

    public void OptionsVisability()
    {
        if (arButton.activeSelf)
        {
            arToolkit.ToggleToolkit();
        }
        else 
        { 
        buttons.SetActive(!buttons.activeSelf);
        }
    }
}
