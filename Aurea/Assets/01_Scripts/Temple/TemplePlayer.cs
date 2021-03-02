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
            tc.SetTrigger(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Podest"))
        {
            tc.SetTrigger(false);
            
        }
    }
}
