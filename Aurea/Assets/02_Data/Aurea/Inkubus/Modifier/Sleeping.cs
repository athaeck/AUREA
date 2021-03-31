using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Sleeping : MonoBehaviour
{
    int sleepRandomFor = 3;
    Aurea aurea = null;

    [SerializeField]
    int roundsLeft = 0;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        roundsLeft = UnityEngine.Random.Range(0, sleepRandomFor);
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
        roundsLeft--;
        if (roundsLeft <= 0)
            Kill();
    }

    public void Kill()
    {
        if (aurea)
        {
            aurea.GetPlayer().SelectedAurea -= BlockAurea;
            aurea.GetPlayer().StartedTurn -= NewTurn;
        }
        DestroyImmediate(this);
    }
}
