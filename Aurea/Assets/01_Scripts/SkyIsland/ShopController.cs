using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private Transform camPosition = null;

    [SerializeField]
    private List<GameObject> spawnPlaces = new List<GameObject>();

    [SerializeField]
    private List<GameObject> prefabItem = new List<GameObject>();

    private List<GameObject> storedPrefab = new List<GameObject>();

    private void Awake()
    {
        ResetItems();
        if(spawnPlaces.Count > 0)
        {
            int i = 0;
            foreach(GameObject spawn in spawnPlaces)
            {
               if(i < storedPrefab.Count)
               {
                    SpawnItems(i,spawn.transform);
               }
               else
               {
                    SpawnItems(0,spawn.transform);
               }
              i++;
            }
        }
    }

    private void ResetItems()
    {
        if(storedPrefab.Count > 0)
        {
            int i = 0;
            foreach(GameObject prefab in storedPrefab)
            {
                Destroy(prefab);
                i++;
            }
        }
    }

    public Transform GetCamPosition()
    {
        return camPosition;
    }

    private void SpawnItems(int index, Transform transform)
    {
        GameObject prefab = Instantiate(prefabItem[index],transform);
        ItemData itemData = prefab.GetComponent<ItemData>();
        itemData.Init("item" + index.ToString(),"schmack schmack",index * index);
        storedPrefab.Add(prefab);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
