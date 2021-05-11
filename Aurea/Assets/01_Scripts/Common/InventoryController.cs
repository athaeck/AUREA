using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private List<PlayerItemData> ownedItems = new List<PlayerItemData>();

    [SerializeField]
    private GameObject _itemObject = null;

    [SerializeField]
    private GameObject _container = null;

    [SerializeField]
    private GameObject inventoryHUD = null;

    private Text _quantity = null;

    private Text _title = null;

    public void InitInventory()
    {
        SetItems();
    }
    private void Start()
    {
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
        foreach(PlayerItemData item in ownedItems)
        {
            GameObject iO = Instantiate(_itemObject,_container.transform.position,_container.transform.rotation,_container.transform);
            InventoryItem iI = iO.AddComponent<InventoryItem>() as InventoryItem;
            iI.SetItem(item.description,item.name,1,this);

        }
        //int i = 0;
        //foreach(GameObject spawnPlace in itemSpawnPlaces)
        //{
        //    if(i < ownedItems.Count)
        //    {
        //        PlayerItemData pid = ownedItems[i];
        //        InventoryItem item = spawnPlace.AddComponent<InventoryItem>() as InventoryItem;
        //        item.SetItem(pid.description,pid.name);

        //    }
        //    else
        //    {
        //        spawnPlace.transform.GetChild(0).gameObject.SetActive(false);
        //        spawnPlace.transform.GetChild(1).gameObject.SetActive(false);
        //    }
        //    i++;
        //}

    }
    public void ChooseItem(string item)
    {
        Debug.Log(item);
    }
}
