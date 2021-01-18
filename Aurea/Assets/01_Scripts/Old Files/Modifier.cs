using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Modifier : Component
{
    [SerializeField]
    private string NAME = "";

    [SerializeField]
    private int ID = 0;

    [SerializeField]
    private string DESCRIPTION = "";

    public Event Event = new Event();

    public void AddEventLitener(Aurea target)
    {
        //TODO
    }

    public string GetName() { return NAME; }
    public int GetID() { return ID; }
    public string GetDescription() { return DESCRIPTION; }

}
