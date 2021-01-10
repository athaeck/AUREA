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

    private GameObject characterPosition = null;

    [SerializeField]
    private GameObject character = null;

    private bool collided = false;

    private bool armode = true;

    private Transform safeCamPosition = null;

    private void Awake()
    {
        if(shopController != null)
        {
            shopCamPosition = shopController.GetCamPosition();
            characterPosition = shopController.GetCharacterPosition();
        }
        ReactiveProps();
        safeCamPosition = cam.transform;
       
    }

    private void ReactiveProps()
    {
        // collided = StateController.Instance.GetCollided();
     //   armode = Player.Instance.GetArMode();
      // armode = true;
    }

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
                exitShopHud.SetActive(true);
                StateController.Instance.SetWalkable(false);
                if(characterPosition != null && shopCamPosition != null && character != null)
                {
                    cam.transform.position = shopCamPosition.transform.position;
                    cam.transform.rotation = shopCamPosition.transform.rotation;
                    character.transform.position = characterPosition.transform.position;
                }
            }
        }
    }
    public void Transfer(ItemData item, GameObject gobject)
    {
        if(shopController != null)   shopController.ActivateItemHUD(item,gobject);
     
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
            if(exitShopHud != null)
            {
                exitShopHud.SetActive(false);
                StateController.Instance.SetWalkable(true);
                if(cam != null)
                {
                    cam.transform.position = safeCamPosition.position;
                    cam.transform.rotation = safeCamPosition.rotation;
                }
            }
        }
    }
}
