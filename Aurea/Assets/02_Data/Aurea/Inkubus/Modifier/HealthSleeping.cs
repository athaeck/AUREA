using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class HealthSleeping : MonoBehaviour
{
    Aurea aurea = null;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.GetPlayer().SelectedAurea += BlockAurea;
        aurea.GetPlayer().StartedTurn += NewTurn;
    }

    void BlockAurea(Aurea _aurea)
    {
        if (aurea == _aurea)
        {
            aurea.GetPlayer().Select(null);
        }
    }

    void NewTurn()
    {
        Kill();
    }

    public void Kill()
    {
        aurea.GetPlayer().SelectedAurea -= BlockAurea;
        aurea.GetPlayer().StartedTurn -= NewTurn;
        DestroyImmediate(this);
    }
}
