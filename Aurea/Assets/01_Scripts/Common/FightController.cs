using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public void TakeInput(Ray ray) {
        Debug.Log("Got input: Fight");
    }
    public void ResetIsland() {
        Debug.Log("Reset Fight");
    }
}
