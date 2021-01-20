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

    public void SetCollided(bool b)
    {
        collided = b;
    }
    public void SetStaticmode(bool b)
    {
        staticmode = b;
    }
    public bool GetCollided()
    {
        return collided;
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
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (staticmode == true)
            {
                if (hit.collider.CompareTag("Item"))
                {
                    ItemData item = hit.collider.gameObject.GetComponent<ItemData>();
                    if (enterController != null) enterController.Transfer(item, hit.collider.gameObject);
                }
            }
            else
            {
                if (hit.collider.CompareTag("Walkable"))
                {
                    Vector3 newDestination = new Vector3(hit.point.x, hit.point.y, hit.point.z);

                    MovementController player = character.GetComponent<MovementController>();
                    player.destination = newDestination;
                }
            }

            if (collided == true)
            {
                if (hit.collider.CompareTag("To-Gameground"))
                {
                    IslandController.Instance.OpenFight();
                }
                if (hit.collider.CompareTag("To-Competition"))
                {
                    IslandController.Instance.OpenFight();
                }
                if (hit.collider.CompareTag("To-AureaSelect"))
                {
                    if (goToPosition != null)
                    {
                        MovementController c = character.GetComponent<MovementController>();
                        Vector3 movement = new Vector3(goToPosition.transform.position.x, goToPosition.transform.position.y, goToPosition.transform.position.z);
                        c.destination = movement;
                        IslandController.Instance.OpenTemple();
                    }
                }
                if (hit.collider.CompareTag("Shop"))
                {
                    enterController.EnterShop();
                }
                if (hit.collider.CompareTag("Inventory"))
                {
                    enterController.EnterInventory();
                }
            }
            if (hit.collider.CompareTag("Difficulty"))
            {
                if (difficultyController != null)
                {
                    difficultyController.SetDifficulty();
                }
            }
        }
    }


    private void Init()
    {
        character.transform.position = spawnPlace.transform.position;

        //cam.transform.position = spawnPlace.transform.position;

        // character.GetComponent<MovementController>().Move(character.transform.position);

        Player.Instance.AddMoney(100);
    }

}
