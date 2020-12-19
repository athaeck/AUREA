using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIHUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject portalHUD = null;

    [SerializeField]
    private GameObject goToPosition = null;

    private Difficulty difficulty;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("To-Gameground"))
        {
            if(portalHUD != null)
            {
                ControlPortal(true,"Durch dieses Portal gelangst du zum Arenakampf");
                StateController.Instance.SetCollided(true);
            }
        }
        if(other.CompareTag("To-Competition"))
        {
            if(portalHUD != null)
            {
                ControlPortal(true,"Durch dieses Portal gelangst du zum Arenakampf");
                StateController.Instance.SetCollided(true);
            }
        }
        if(other.CompareTag("Shop"))
        {
           StateController.Instance.SetCollided(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("To-Gameground"))
        {
            ControlPortal(false);
            StateController.Instance.SetCollided(false);
        }
        if(other.CompareTag("To-Competition"))
        {
            ControlPortal(false);
            StateController.Instance.SetCollided(false);
        }
        if(other.CompareTag("Shop"))
        {
            StateController.Instance.SetCollided(false);
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
    private void ControlPortal(bool state)
    {
        portalHUD.SetActive(state);
    }
    private void Update()
    {
        difficulty = Player._instance.GetDifficulty();
    }

}
