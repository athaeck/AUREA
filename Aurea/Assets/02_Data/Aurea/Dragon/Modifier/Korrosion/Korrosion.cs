using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Korrosion : MonoBehaviour
{
    private float damageMultiplier = 1.2f;
    private Aurea aurea = null;

    private int amountOfRounds = 5;

    private int roundsLeft = 0;
    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.BeforeGettingHit += ApplyDamage;
        aurea.GetPlayer().StartedTurn += NewTurn;
        roundsLeft = amountOfRounds;
    }

    void NewTurn()
    {
        roundsLeft--;
        if (roundsLeft <= 0)
            Kill();
    }

    void ApplyDamage(Damage _dmg)
    {
        _dmg.physicalDamage *= damageMultiplier;
        _dmg.magicalDamage *= damageMultiplier;
    }

    public void Kill()
    {
        if (aurea)
        {
            aurea.BeforeGettingHit -= ApplyDamage;
            
        }
        
        DestroyImmediate(this);
    }
}
