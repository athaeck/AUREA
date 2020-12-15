using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

//[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    private float waitBetweenClicks = 1f;

   // private PlayerController target = null;

    bool justClicked = false;

    //Mit dennis das über das singletzon holen
    [SerializeField]
    private MovementController character = null;

    private bool staticmode = false;

    private bool collided = false;

    [SerializeField]
    private EnterController enterController = null;


    void Start()
    {
       // target = GetComponent<PlayerController>();
    }
    
    void FixedUpdate()
    {
        ReactiveProps();
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        //ReactiveProps();
        if ( justClicked) { return; }

        Ray ray;

        if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
        }
        else if (Input.GetMouseButton(0)) 
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else { return; }

        if(Physics.Raycast(ray,out RaycastHit hit,float.MaxValue,layerMask))
        {
            if(EventSystem.current.IsPointerOverGameObject())
                return;

            Aurea hero = null;
            if(hit.collider.CompareTag("Aurea"))
            {
                hero = hit.collider.GetComponent<Aurea>();
                StartCoroutine(WaitBetweetClick());
            }
            // target.Select(hero);

            if(hit.collider.CompareTag("EndTurn"))
            {
                //target.ManuallyEndTurn();
                StartCoroutine(WaitBetweetClick());
            }

            if(hit.collider.CompareTag("Inventory"))
            {
                Debug.Log("Open Inventory");
                StartCoroutine(WaitBetweetClick());
            }
            if(staticmode == false)
            {

            }
            else
            {
                if(hit.collider.CompareTag("Walkable"))
                {
                    MoveCharacter(hit);
                }
            }

            if(collided == true)
            { 
               if(hit.collider.CompareTag("To-Gameground"))
               {
                    IslandController.Instance.ChangeActiveIsland(Island.ChickenFight);
               }
               if(hit.collider.CompareTag("To-Competition"))
               {
                    IslandController.Instance.ChangeActiveIsland(Island.ChickenFight);
               }
                if(hit.collider.CompareTag("Shop"))
                {
                    enterController.EnterShop();
                }
            }
        }
    }

    private void ReactiveProps()
    {
        staticmode = StateController.Instance.GetWalkability();
        collided = StateController.Instance.GetCollided();
    }
    private void MoveCharacter(RaycastHit hit)
    {
        Vector3 movement = new Vector3(hit.point.x,hit.point.y,hit.point.z);
        character.Move(movement);
    }

    IEnumerator WaitBetweetClick()
    {
        justClicked = true;
        yield return new WaitForSeconds(waitBetweenClicks * Time.deltaTime);
        justClicked = false;
    }
}
