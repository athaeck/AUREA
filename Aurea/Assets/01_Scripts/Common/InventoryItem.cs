using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private string _title;
    private string _description;

    public void SetItem( string d, string t)
    {
        _description = d;
        _title = t;
    }
    private void Start()
    {
        GameObject title;
        GameObject button;
        title = this.transform.GetChild(1).gameObject;
        button = this.transform.GetChild(0).gameObject;

        if(title != null) {
            Text titleText = title.GetComponentInChildren<Text>();
            if(titleText != null)
            {
                titleText.text = _title;
            }
        }
        if(button != null) {
            Text buttonText = button.GetComponentInChildren<Text>();
            if(buttonText != null)
            {
                buttonText.text = "Auswahl";
            }
        }

    }

}
