using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplePlayer : MonoBehaviour
{
    [SerializeField]
    TempleController tc = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Watch"))
        {
            FindObjectOfType<AudioController>()?.Play("monster");
            tc.WatchTrigger(true, other.gameObject);
        }
        if (other.CompareTag("To-SkyIsland"))
        {
            tc.teleport(true);
        }
        if (other.CompareTag("Unlock"))
        {
            tc.UnlockTrigger(true, other.gameObject);
        }
        if (other.CompareTag("ResetPosition"))
        {
            other.gameObject.GetComponent<ResetController>()?.RespwanPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Watch"))
        {
            tc.WatchTrigger(false, null);

        }
        if (other.CompareTag("To-SkyIsland"))
        {
            tc.teleport(false);
        }
        if (other.CompareTag("Unlock"))
        {
            tc.UnlockTrigger(false, other.gameObject);
        }
    }
}
