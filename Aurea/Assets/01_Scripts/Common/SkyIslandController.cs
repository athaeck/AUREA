using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class SkyIslandController : MonoBehaviour
{

    [SerializeField]
    private GameObject spawnPlace = null;

    [SerializeField]
    private GameObject character = null;

    [SerializeField]
    private EnterController enterController = null;

    [SerializeField]
    private GameObject goToPosition = null;

    [SerializeField]
    private DifficultyController difficultyController = null;

    private bool staticmode = false;

    private bool collided = false;

    private CollisionInteractable collidedWith = CollisionInteractable.None;


    private void Start()
    {
        if (spawnPlace != null && character != null)
        {
            Init();
        }
    }
    private void Update()
    {

    }

    public void SetCollided(bool b, CollisionInteractable _collision)
    {
        collided = b;
        collidedWith = _collision;
    }

    public void RemoveCollision()
    {
        collided = false;
        collidedWith = CollisionInteractable.None;
    }

    public void SetStaticmode(bool b)
    {
        staticmode = b;
    }
    public bool GetCollided()
    {
        return collided;
    }

    public CollisionInteractable GetCollidedWith()
    {
        return collidedWith;
    }

    public bool GetStaticMode()
    {
        return staticmode;
    }


    public void ResetIsland()
    {
        Init();
    }

    public void TakeInput(Ray ray)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        bool actionExecuted = false;
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {

            if (staticmode == true)
            {
                if (hit.collider.CompareTag("Item"))
                {
                    ItemData item = hit.collider.gameObject.GetComponent<ItemData>();
                    if (enterController != null) enterController.Transfer(item, hit.collider.gameObject);
                    actionExecuted = true;
                }
            }

            if (collided == true)
            {
                if (hit.collider.CompareTag("To-Gameground") && collidedWith == CollisionInteractable.Fight)
                {
                    IslandController.Instance.OpenFight();
                    actionExecuted = true;
                }
                if (hit.collider.CompareTag("To-Competition") && collidedWith == CollisionInteractable.Competition)
                {
                    IslandController.Instance.OpenFight();
                    actionExecuted = true;
                }
                if (hit.collider.CompareTag("To-AureaSelect") && collidedWith == CollisionInteractable.Select)
                {
                    if (goToPosition != null)
                    {
                        // MovementController c = character.GetComponent<MovementController>();
                        // Vector3 movement = new Vector3(goToPosition.transform.position.x, goToPosition.transform.position.y, goToPosition.transform.position.z);
                        // c.destination = movement;
                        IslandController.Instance.OpenTemple();
                        actionExecuted = true;
                    }
                }
                if (hit.collider.CompareTag("Shop") && collidedWith == CollisionInteractable.Shop)
                {
                    enterController.EnterShop();
                    actionExecuted = true;
                }
                if (hit.collider.CompareTag("Inventory") && collidedWith == CollisionInteractable.Inventory)
                {
                    enterController.EnterInventory();
                    actionExecuted = true;
                }
            }
            if (hit.collider.CompareTag("Difficulty"))
            {
                if (difficultyController != null)
                {
                    difficultyController.SetDifficulty();
                    actionExecuted = true;
                }
            }
        }

        if (!actionExecuted && !staticmode)
        {
            Plane hitPlane = new Plane(Vector3.up, character.transform.position);

            if (hitPlane.Raycast(ray, out float distance))
            {
                Vector3 direction = ray.GetPoint(distance);

                MovementController player = character.GetComponent<MovementController>();
                player.Move(direction);
            }
        }
    }

    public void ResetPlayerPosition()
    {
        MovementController movementController = character.GetComponent<MovementController>();
        character.transform.position = spawnPlace.transform.position;
    }


    private void Init()
    {
        ResetPlayerPosition();

        //cam.transform.position = spawnPlace.transform.position;

        // character.GetComponent<MovementController>().Move(character.transform.position);

        Player.Instance.AddMoney(100);
    }

}
