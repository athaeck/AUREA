using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterController : MonoBehaviour
{
    [SerializeField]
    private ShopController shopController = null;

    private Transform shopCamPosition = null;

    [SerializeField]
    private Camera cam = null;

    [SerializeField]
    private GameObject exitShopHud = null;

    private bool collided = false;

    private bool armode = true;

    private Transform safeCamPosition = null;

    private void Awake()
    {
        if(shopController != null)
        {
            shopCamPosition = shopController.GetCamPosition();
        }
        ReactiveProps();
    }

    private void ReactiveProps()
    {
        collided = StateController.Instance.GetCollided();
       armode = Player.Instance.GetArMode();
    }

    // Update is called once per frame
    void Update()
    {
        ReactiveProps();
    }
    public void EnterShop()
    {
        if(armode == true)
        {
            if(exitShopHud != null)
            {
                StateController.Instance.SetWalkable(false);
                exitShopHud.SetActive(true);
                cam.cullingMask = 1 << 9;
            }
        }
        else
        {
            if(exitShopHud != null)
            {
                StateController.Instance.SetWalkable(false);
               
            }
        }
    }
    public void ExitShop()
    {
        if(armode == true)
        {
            if(exitShopHud != null)
            {
                StateController.Instance.SetWalkable(true);
                exitShopHud.SetActive(false);
                cam.cullingMask = LayerMask.NameToLayer("Everything");
            }
        }
        else
        {

        }
    }
}
