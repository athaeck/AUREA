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

    private bool clicked = false;

    public void InitInventory()
    {
        if(!clicked)
        {
            SetItems();
        }
        clicked = true;
    }
    private void Start()
    {
    }
    public void Exit()
    {
        if(inventoryHUD != null)
        {
            inventoryHUD.SetActive(false);
            clicked = false;
            List<GameObject> items = new List<GameObject>();
            int length = _container.transform.childCount;
            for(int i = 0 ; i < length ; i++)
            {
                items.Add(_container.transform.GetChild(i).transform.gameObject);
            }
            for(int i = length-1 ; i >= 0 ; i--)
            {
                Destroy(items[i]);
            }
        }
    }
    private void Update()
    {
       //ownedItems = Player.Instance.GetItems();
    }
    private void SetItems()
    {
        ownedItems = Player.Instance.GetItems();
        if(inventoryHUD != null)
        {
            inventoryHUD.SetActive(true);
        }
        int index = 0;
        foreach(PlayerItemData item in ownedItems)
        {
            int quantity = CheckQuantity(item,index);
            GameObject iO = Instantiate(_itemObject,_container.transform.position,_container.transform.rotation,_container.transform);
            InventoryItem iI = iO.AddComponent<InventoryItem>();
            iI.SetItem(item.description,item.name,quantity,this);
            index++;
        }
    }
    public void ChooseItem(string item)
    {
        Debug.Log(item);
    }
    private int CheckQuantity(PlayerItemData item, int currentIndex) {
        int quantity = 1;
        int index = 0;
        foreach(PlayerItemData it in ownedItems)
        {
            if(currentIndex != index)
            {
                if(item.name == it.name)
                {

                    quantity++;

                }
            }
            index++;
        }
        return quantity;
    }
}
