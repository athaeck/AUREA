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
    private Image image = null;

    [SerializeField]
    private GameObject hud = null;


    private void SetTitle(string s)
    {
        if (title != null)
        {
            title.text = s;
        }
    }

    private void SetDescription(string s)
    {
        if (description != null)
        {
            description.text = s;
        }
    }

    private void SetPrice(string s)
    {
        if (price != null)
        {
            price.text = "Kaufen für " + s + " Münzen";
        }
    }

    private void ActivateHUD(bool b)
    {
        if (hud != null)
        {
            hud.SetActive(b);
        }
    }

    private void SetPosition(Transform position)
    {
        transform.position = position.position;
    }
    private void SetImage(Sprite img)
    {
        if (image != null)
        {
            image.sprite = img;
        }
    }

    public void CloseHUD()
    {
        if (hud != null)
        {
            hud.SetActive(false);
        }
    }
    public void Init(string t, string d, string p, bool b, Sprite image, Transform tr)
    {
        SetTitle(t);
        SetDescription(d);
        SetPrice(p);
        ActivateHUD(b);
        SetPosition(tr);
        SetImage(image);
    }
}
