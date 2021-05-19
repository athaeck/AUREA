using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    private GameObject _optionsHud = null;

    [SerializeField]
    private GameObject exitShopHud = null;

    private GameObject characterPosition = null;

    [SerializeField]
    private GameObject character = null;

    [SerializeField]
    private InventoryController inventoryController = null;

    private bool collided = false;

    private bool armode = true;

    [SerializeField]
    private Transform safeCamPosition = null;

    private void Awake()
    {
        if (shopController != null)
        {
            shopCamPosition = shopController.GetCamPosition();
            characterPosition = shopController.GetCharacterPosition();
        }
        if (skyIslandController != null)
        {
            ReactiveProps();
        }

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
        CloseOptionsHud(false);
        if (armode == true)
        {
            if (exitShopHud != null && skyIslandController != null)
            {
                skyIslandController.SetStaticmode(true);
                exitShopHud.SetActive(true);
                cam.cullingMask = 1 << 9;
            }
        }
        else
        {
            if (exitShopHud != null)
            {
                exitShopHud.SetActive(true);
                skyIslandController.SetStaticmode(true);
                if (characterPosition != null && shopCamPosition != null && character != null)
                {
                    FollowTarget followTarget = cam.GetComponent<FollowTarget>();
                    followTarget.TakeTarget(null);
                    cam.transform.position = shopCamPosition.transform.position;
                    cam.transform.rotation = shopCamPosition.transform.rotation;
                    character.transform.position = characterPosition.transform.position;

                }
            }
        }
    }
    public void Transfer(ItemData item, GameObject gobject)
    {
        if (shopController != null) shopController.ActivateItemHUD(item, gobject);

    }

    public void EnterInventory()
    {
        CloseOptionsHud(false);
        if (inventoryController != null)
        {
            inventoryController.InitInventory();
        }
    }

    public void ExitInventory()
    {
        CloseOptionsHud(true);
        if (inventoryController != null)
        {
            inventoryController.Exit();
        }
    }

    public void ExitShop()
    {
        CloseOptionsHud(true);
        if (armode == true)
        {
            if (exitShopHud != null)
            {
                skyIslandController.SetStaticmode(false);
                exitShopHud.SetActive(false);
                cam.cullingMask = LayerMask.NameToLayer("Everything");
            }
        }
        else
        {
            if (exitShopHud != null)
            {
                exitShopHud.SetActive(false);
                skyIslandController.SetStaticmode(false);
                if (shopController != null)
                {
                    ItemHUDController ihc = shopController.GetItemHUDController();
                    ihc.CloseHUD();
                }
                if (cam != null)
                {
                    FollowTarget followTarget = cam.GetComponent<FollowTarget>();
                    followTarget.TakeTarget(character.transform);
                }
            }
        }
    }
    private void CloseOptionsHud(bool b)
    {
        if (_optionsHud != null)
        {
            _optionsHud.SetActive(b);
        }
    }
}
