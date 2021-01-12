using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIHUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject portalHUD = null;

    [SerializeField]
    private SkyIslandController skyIslandController = null;

    private Difficulty difficulty;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("To-Gameground"))
        {
            if(portalHUD != null)
            {
                ControlPortal(true,"Durch dieses Portal gelangst du zum Playground");
                if(skyIslandController != null)
                {
                    skyIslandController.SetCollided(true);
                }
            }
        }
        if(other.CompareTag("To-AureaSelect"))
        {
            if(portalHUD != null)
            {
                ControlPortal(true,"Durch dieses Portal gelangst du zur Aurea Auswahl");
                if(skyIslandController != null)
                {
                    skyIslandController.SetCollided(true);
                }
            }
        }
        if(other.CompareTag("To-Competition"))
        {
            if(portalHUD != null)
            {
                ControlPortal(true,"Durch dieses Portal gelangst du zum Arenakampf");
                if(skyIslandController != null)
                {
                    skyIslandController.SetCollided(true);
                }
            }
        }
        if(other.CompareTag("Shop"))
        {
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(true);
            }
        }
        if(other.CompareTag("Inventory"))
        {
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("To-Gameground"))
        {
            ControlPortal(false);
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(false);
            }
        }
        if(other.CompareTag("To-AureaSelect"))
        {
            ControlPortal(false);
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(false);
            }
        }
        if(other.CompareTag("To-Competition"))
        {
            ControlPortal(false);
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(false);
            }
        }
        if(other.CompareTag("Shop"))
        {
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(false);
            }
        }
        if(other.CompareTag("Inventory"))
        {
            if(skyIslandController != null)
            {
                skyIslandController.SetCollided(false);
            }
        }

    }

    private void ControlPortal(bool state,string message)
    {
        portalHUD.SetActive(state);
       if(state == true)
        {
            portalHUD.transform.position = transform.position;
            PortalController pc = portalHUD.GetComponent<PortalController>();
            pc.SetHUD(message);
        }
    }
    private void ControlPortal(bool state)
    {
        portalHUD.SetActive(state);
    }
    private void Update()
    {
        difficulty = Player.Instance.GetDifficulty();
    }

}
