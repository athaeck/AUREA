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
    private SkyIslandController skyIslandController = null;

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
        if(skyIslandController != null)
        {
            ReactiveProps();
        }
        safeCamPosition = cam.transform;
       
    }

    private void ReactiveProps()
    {
        collided = skyIslandController.GetCollided();
        armode = Player.Instance.IsArOn();
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
            if(exitShopHud != null && skyIslandController != null)
            {
                //StateController.Instance.SetWalkable(false);
                skyIslandController.SetStaticmode(true);
                exitShopHud.SetActive(true);
                cam.cullingMask = 1 << 9;
            }
        }
        else
        {
            if(exitShopHud != null)
            {
                exitShopHud.SetActive(true);
                // StateController.Instance.SetWalkable(false);
                skyIslandController.SetStaticmode(true);
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

    public void EnterInventory()
    {

    }

    public void ExitInventory()
    {

    }

    public void ExitShop()
    {
        if(armode == true)
        {
            if(exitShopHud != null)
            {
                //StateController.Instance.SetWalkable(true);
                skyIslandController.SetStaticmode(false);
                exitShopHud.SetActive(false);
                cam.cullingMask = LayerMask.NameToLayer("Everything");
            }
        }
        else
        {
            if(exitShopHud != null)
            {
                exitShopHud.SetActive(false);
                // StateController.Instance.SetWalkable(true);
                skyIslandController.SetStaticmode(false);
                if(cam != null)
                {
                    cam.transform.position = safeCamPosition.position;
                    cam.transform.rotation = safeCamPosition.rotation;
                }
            }
        }
    }
}
