using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    [SerializeField]
    private string title = null;

    [SerializeField]
    private Sprite image = null;

    [SerializeField]
    private string description = null;
    [SerializeField]
    private int price = 0;

    public void Init(string _Title, string _Description, int _Price)
    {
        title = _Title;
        description = _Description;
        price = _Price;
    }
    public string GetTitle()
    {
        return title;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetPrice()
    {
        return price;
    }
    public Sprite GetImage()
    {
        return image;
    }

}
