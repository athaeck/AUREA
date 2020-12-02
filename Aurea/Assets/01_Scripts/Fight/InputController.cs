using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Player))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    private float waitBetweenClicks = 1f;

    private Player target = null;

    bool justClicked = false;

    
    void Start()
    {
        target = GetComponent<Player>();
    }
    
    void Update()
    {
        if (!target || justClicked) { return; }

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

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Aurea hero = null;
            if (hit.collider.CompareTag("Aurea"))
            {
                hero = hit.collider.GetComponent<Aurea>();
                StartCoroutine(WaitBetweetClick());
            }
            target.Select(hero);

            if (hit.collider.CompareTag("EndTurn"))
            {
                target.ManuallyEndTurn();
                StartCoroutine(WaitBetweetClick());
            }

            if (hit.collider.CompareTag("Inventar"))
            {
                Debug.Log("Open Inventar");
                StartCoroutine(WaitBetweetClick());
            }
        }
    }

    IEnumerator WaitBetweetClick()
    {
        justClicked = true;
        yield return new WaitForSeconds(waitBetweenClicks * Time.deltaTime);
        justClicked = false;
    }
}
