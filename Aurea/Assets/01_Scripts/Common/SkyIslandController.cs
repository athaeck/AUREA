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

    [SerializeField]
    private List<GameObject> AtStartDisabledHuds = null;

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
        character.transform.position = spawnPlace.transform.position;
    }


    private void Init()
    {
        ResetPlayerPosition();

        DisabledHudsAtStart();

        //cam.transform.position = spawnPlace.transform.position;

        // character.GetComponent<MovementController>().Move(character.transform.position);

        Player.Instance.AddMoney(100);
    }
    private void DisabledHudsAtStart()
    {
        foreach (GameObject hud in AtStartDisabledHuds)
        {
            hud.SetActive(false);
        }
        if (enterController != null)
        {
            enterController.ExitShop();
        }
    }

}
