using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplePlayer : MonoBehaviour
{
    [SerializeField]
    TempleController tc = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Podest"))
        {
            tc.SetTrigger(true,other.gameObject);
        }
        if (other.CompareTag("To-SkyIsland"))
        {
            tc.teleport(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Podest"))
        {
            tc.SetTrigger(false, null);
            
        }
        if (other.CompareTag("To-SkyIsland"))
        {
            tc.teleport(false);
        }
    }
}
