using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleController : MonoBehaviour
{
    public void TakeInput(Ray ray) {
        Debug.Log("Got input: Temple");
    }
    public void ResetIsland()
    {
        Debug.Log("Reset Temple");
    }
}
