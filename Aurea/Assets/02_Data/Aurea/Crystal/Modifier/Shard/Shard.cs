using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Shard : MonoBehaviour
{
    Aurea aurea = null;
    void Start()
    {
        aurea = GetComponent<Aurea>();
        Debug.Log("Found Aurea");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
