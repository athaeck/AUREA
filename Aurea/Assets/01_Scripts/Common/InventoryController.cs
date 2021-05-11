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
        int i = 0;
        foreach(PlayerItemData item in ownedItems)
        {
            int x = 1;
            int y = 0;
            foreach(PlayerItemData it in ownedItems)
            {
                if(item.name.Equals(it.name))
                {
                    if(i != y)
                    {
                        x++;
                    }
                }
            }
            GameObject iO = Instantiate(_itemObject,_container.transform.position,_container.transform.rotation,_container.transform);
            InventoryItem iI = iO.AddComponent<InventoryItem>() as InventoryItem;
            iI.SetItem(item.description,item.name,x,this);
            i++;
        }
    }
    public void ChooseItem(string item)
    {
        Debug.Log(item);
    }
}
