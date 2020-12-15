using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUDController : MonoBehaviour
{
    [SerializeField]
    private Text title = null;

    [SerializeField]
    private Text description = null;

    [SerializeField]
    private Text price = null;

    [SerializeField]
    private GameObject hud = null;


    public void SetTitle(string s)
    {
        if(title != null)
        {
            title.text = s;
        }
    }

    public void SetDescription(string s)
    {
        if(description != null)
        {
            description.text = s;
        }
    }

    public void SetPrice(string s)
    {
        if(price != null)
        {
            price.text = s;
        }
    }

    public void ActivateHUD(bool b)
    {
        if(hud != null)
        {
            hud.SetActive(b);
        }
    }

    public void CloseHUD()
    {
        if(hud != null)
        {
            hud.SetActive(false);
        }
    }
}
