using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

//[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour
{

    [SerializeField]
    private float waitBetweenClicks = 5f;

    // private PlayerController target = null;

    bool justClicked = false;


    void Start()
    {
        // target = GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {

        //ReactiveProps();
        if(justClicked) { return; }

        Ray ray;

        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ray = Camera.main.ScreenPointToRay(touch.position);
        }
        else if(Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        else { return; }

        switch(IslandController.Instance.activeIsland)
        {
            case Island.SkyIsland:
                IslandController.Instance.skyIsland.TakeInput(ray);
                break;
            case Island.TempleOfDoom:
                IslandController.Instance.temple.TakeInput(ray);
                break;
            case Island.ChickenFight:
                IslandController.Instance.fight.TakeInput(ray);
                break;
        }
        StartCoroutine(WaitBetweetClick());
    }

      

    IEnumerator WaitBetweetClick()
    {
        justClicked = true;
        yield return new WaitForSeconds(waitBetweenClicks * Time.deltaTime);
        justClicked = false;
    }
}
