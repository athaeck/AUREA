using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(MovementController))]
public class SelectInputController : MonoBehaviour
{
    [SerializeField]
    private float waitBetweenClicks = 1f;

    bool justClicked = false;


    [SerializeField]
    private GameObject selectAureaHUD = null;

    [SerializeField]
    private GameObject viewAureaHUD = null;

    [SerializeField]
    private ButtonSelect buttonSelect = null;

    private MovementController movementController = null;

    private bool trigger = false;

    void Start()
    {
        movementController = GetComponent<MovementController>();
    }

    void Update()
    {
        if (justClicked ) { return; }
        if (!trigger) { return; }
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
            if (hit.collider.CompareTag("Aurea") && !viewAureaHUD.activeSelf)
            {
                hero = hit.collider.GetComponent<Aurea>();
                StartCoroutine(WaitBetweetClick());
                buttonSelect.select(hero.GetName());
                selectAureaHUD.GetComponent<FollowTarget>().TakeTarget(hero.transform);

                selectAureaHUD.SetActive(true);
            }
            else
            {
                selectAureaHUD.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        float rotation = Input.GetAxis("Horizontal");
        float movement = Input.GetAxis("Vertical");

        //movementController.Rotate(rotation);
       // movementController.Move(movement);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Aurea"))
        {
            trigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Aurea"))
        {
            trigger = false;
            selectAureaHUD.SetActive(false);
        }
    }


    IEnumerator WaitBetweetClick()
    {
        justClicked = true;
        yield return new WaitForSeconds(waitBetweenClicks * Time.deltaTime);
        justClicked = false;
    }
}
