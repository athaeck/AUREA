using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Rinde : MonoBehaviour
{
    Aurea aurea = null;
    float shieldMultiplier = 0.7f;

    int roundsLeft = 0;
    int amountOfRounds = 3;

    void Start()
    {
        roundsLeft = amountOfRounds;
        aurea = GetComponent<Aurea>();
        aurea.BeforeGettingHit += Shield;
        aurea.GetPlayer().StartedTurn += NewTurn;
    }

    void Shield(Damage _dmg)
    {
        _dmg.magicalDamage *= shieldMultiplier;
        _dmg.physicalDamage *= shieldMultiplier;
    }

    void NewTurn()
    {
        roundsLeft--;
        if (roundsLeft <= 0)
            Kill();
    }

    public void Kill()
    {
        aurea.BeforeGettingHit -= Shield;
        aurea.GetPlayer().StartedTurn -= NewTurn;
        DestroyImmediate(this);
    }
}