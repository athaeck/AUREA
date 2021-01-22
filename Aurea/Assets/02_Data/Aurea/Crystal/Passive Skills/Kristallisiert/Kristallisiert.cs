using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Kristallisiert : MonoBehaviour
{
    Aurea aurea = null;
    int splitter = 0;
    int neededSplitter = 5;
    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.StartAttack += AddShardSplitter;
    }

    void AddShardSplitter(Damage _dmg)
    {
        splitter++;

        if (splitter >= neededSplitter)
            StartSplitterAttack();
    }

    void StartSplitterAttack()
    {
        Debug.Log("Start Splitter Attack");
    }
}
