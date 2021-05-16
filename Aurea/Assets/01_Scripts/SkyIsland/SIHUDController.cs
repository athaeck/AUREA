using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionInteractable
{
    None,
    Shop,
    Inventory,
    Fight,
    Select,
    Competition
}

public class SIHUDController : MonoBehaviour
{
    [SerializeField]
    private GameObject portalHUD = null;

    [SerializeField]
    private SkyIslandController skyIslandController = null;

    private Difficulty difficulty;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("To-Gameground"))
        {
            if (portalHUD != null)
            {
                ControlPortal(true, "Kämpfen", Island.ChickenFight, "Auf in den Kampf");
                if (skyIslandController != null)
                {
                    skyIslandController.SetCollided(true, CollisionInteractable.Fight);
                }
            }
        }
        if (other.CompareTag("To-AureaSelect"))
        {
            if (portalHUD != null)
            {
                ControlPortal(true, "Betreten", Island.TempleOfDoom, "Wähle dein Team aus");
                if (skyIslandController != null)
                {
                    skyIslandController.SetCollided(true, CollisionInteractable.Select);
                }
            }
        }
        if (other.CompareTag("To-Competition"))
        {
            if (portalHUD != null)
            {
                ControlPortal(true, "Betreten", Island.TempleOfDoom, "Wähle dein Team aus");
                if (skyIslandController != null)
                {
                    skyIslandController.SetCollided(true, CollisionInteractable.Competition);
                }
            }
        }
        if (other.CompareTag("Shop"))
        {
            if (skyIslandController != null)
            {
                skyIslandController.SetCollided(true, CollisionInteractable.Shop);
            }
        }
        if (other.CompareTag("Inventory"))
        {
            if (skyIslandController != null)
            {
                skyIslandController.SetCollided(true, CollisionInteractable.Inventory);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("To-Gameground"))
        {
            ControlPortal(false);
            if (skyIslandController != null)
            {
                skyIslandController.RemoveCollision();
            }
        }
        if (other.CompareTag("To-AureaSelect"))
        {
            ControlPortal(false);
            if (skyIslandController != null)
            {
                skyIslandController.RemoveCollision();
            }
        }
        if (other.CompareTag("To-Competition"))
        {
            ControlPortal(false);
            if (skyIslandController != null)
            {
                skyIslandController.RemoveCollision();
            }
        }
        if (other.CompareTag("Shop"))
        {
            if (skyIslandController != null)
            {
                skyIslandController.RemoveCollision();
            }
        }
        if (other.CompareTag("Inventory"))
        {
            if (skyIslandController != null)
            {
                skyIslandController.RemoveCollision();
            }
        }

    }

    private void ControlPortal(bool state, string message, Island island, string instruction)
    {
        portalHUD.SetActive(state);
        if (state == true)
        {
            portalHUD.transform.position = transform.position;
            PortalController pc = portalHUD.GetComponent<PortalController>();
            pc.SetDescription(message, island, instruction);
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
