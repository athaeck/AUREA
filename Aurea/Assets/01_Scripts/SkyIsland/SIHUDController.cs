using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIHUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject portalHUD = null;

    private Difficulty difficulty;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("To-Gameground"))
        {
            if(portalHUD != null)
            {
                ControlPortal(true,"Durch dieses Portal gelangst du zum Arenakampf");
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("To-Gameground"))
        {
            ControlPortal(false,null);
        }

    }

    private void ControlPortal(bool state,string message)
    {
        portalHUD.SetActive(state);
       if(state == true)
        {
            portalHUD.transform.position = transform.position;
            PortalController pc = portalHUD.GetComponent<PortalController>();
            pc.SetHUD(difficulty,message);
        }
    }
    private void Update()
    {
        difficulty = Player._instance.GetDifficulty();
    }

}
