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
        aurea.AfterGettingHit += CheckAfterHit;
        aurea.GetPlayer().StartedTurn += NewTurn;
    }

    void CheckGettingHit(Damage _dmg)
    {
        Debug.Log("About to Compare");
        if (_dmg.sender.GetAureaData().NAME == "Crystal")
            _dmg.physicalDamage *= dmgMultiplier;
    }

    void CheckAfterHit(Damage _dmg) {
        if (_dmg.sender.GetAureaData().NAME == "Crystal")
            _dmg.physicalDamage *= (1 / dmgMultiplier);
    }

    void NewTurn()
    {
        activeRounds--;

        if(activeRounds <= 0)
            Destroy(this);
    }

}
