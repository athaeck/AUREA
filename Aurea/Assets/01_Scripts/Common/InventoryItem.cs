using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private string _title;
    private string _description;
    private int _quantity;
    private InventoryController _inventoryController = null;

    public void SetItem( string d, string t,int q, InventoryController inventoryController)
    {
        _description = d;
        _title = t;
        _quantity = q;
        _inventoryController = inventoryController;
    }
    private void ClickHandler()
    {
        if(_inventoryController != null)
        {
            _inventoryController.ChooseItem(_title);
        }
    }
    private void Start()
    {
        GameObject quantity = this.transform.GetChild(0).gameObject;
        GameObject title = this.transform.GetChild(1).gameObject;
        Button button = this.transform.GetChild(2).gameObject.GetComponent<Button>();
        if(quantity != null)
        {
            quantity.GetComponent<Text>().text = _quantity.ToString();
        }
        if(title != null)
        {
            title.GetComponentInChildren<Text>().text = _title;
        }
        if(button != null)
        {
            button.onClick.AddListener(ClickHandler);
        }

    }

}
