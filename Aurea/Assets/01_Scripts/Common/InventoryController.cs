using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private List<PlayerItemData> ownedItems = new List<PlayerItemData>();

    [SerializeField]
    private List<GameObject> itemSpawnPlaces = new List<GameObject>();

    [SerializeField]
    private GameObject inventoryHUD = null;

    public void InitInventory()
    {
        SetItems();
    } 
    public void Exit()
    {
        if(inventoryHUD != null)
        {
            inventoryHUD.SetActive(false);
        }
    }
    private void Update()
    {
       ownedItems = Player.Instance.GetItems();
    }
    private void SetItems()
    {
        if(inventoryHUD != null)
        {
            inventoryHUD.SetActive(true);
        }
        int i = 0;
        Text buttonText;
        foreach(GameObject spawnPlace in itemSpawnPlaces)
        {
            if(i < ownedItems.Count)
            {
                 PlayerItemData item = ownedItems[i];
                buttonText = spawnPlace.GetComponentInChildren<Text>();
                buttonText.text = item.name;

            }
            else
            {
                buttonText = spawnPlace.GetComponentInChildren<Text>();
                buttonText.text = "";
            }
            i++;
        }
    }
    public void ChooseItem()
    {
        
    }
}
