using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Shard : MonoBehaviour
{
    Aurea aurea = null;
    float dmgMultiplier = 1.5f;
    int activeRounds = 3;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.BeforeGettingHit += CheckGettingHit;
        aurea.GetPlayer().StartedTurn += NewTurn;
    }

    void CheckGettingHit(Damage _dmg)
    {
        if (_dmg.sender.GetAureaData().NAME == "Crystal")
            _dmg.physicalDamage *= dmgMultiplier;
    }

    public void Kill() {
        aurea.BeforeGettingHit -= CheckGettingHit;
        aurea.GetPlayer().StartedTurn -= NewTurn;
        DestroyImmediate(this);
    }

    void NewTurn()
    {
        activeRounds--;

        if(activeRounds <= 0)
            DestroyImmediate(this);
    }

}
