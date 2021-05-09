using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
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
        foreach(GameObject spawnPlace in itemSpawnPlaces)
        {
            if(i < ownedItems.Count)
            {
                PlayerItemData pid = ownedItems[i];
                InventoryItem item = spawnPlace.AddComponent<InventoryItem>() as InventoryItem;
                item.SetItem(pid.description,pid.name);

            }
            else
            {
                spawnPlace.transform.GetChild(0).gameObject.SetActive(false);
                spawnPlace.transform.GetChild(1).gameObject.SetActive(false);
            }
            i++;
        }
    }
    public void ChooseItem(Text item)
    {
        Debug.Log(item.text);
    }
}
