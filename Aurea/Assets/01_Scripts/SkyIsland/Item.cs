using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string title = null;

    private string description = null;

    private int price = 0;

    public void Init(string _Title, string _Description, int _Price)
    {
        title = _Title;
        description = _Description;
        price = _Price;
    }

}
