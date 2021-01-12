using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private List<PlayerItemData> ownedItems = new List<PlayerItemData>();

    [SerializeField]
    private List<GameObject> itemSpawnPlaces = new List<GameObject>();

    private void Start()
    {
        SetItems();
        Debug.Log("Hud startet"); 
        ownedItems = Player.Instance.GetItems();
        Debug.Log("got items from data");
    }
    private void Update()
    {
       
    }
    private void SetItems()
    {
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
