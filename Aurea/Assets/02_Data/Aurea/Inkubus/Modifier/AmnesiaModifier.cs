using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class AmnesiaModifier : MonoBehaviour
{
    [SerializeField]
    float damageMultiplier = 0.3f;
    int doDamageForRounds = 5;
    Aurea aurea = null;
    int roundsLeft = 0;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        roundsLeft = doDamageForRounds;
        aurea.GetPlayer().StartedTurn += NewTurn;
    }

    void NewTurn()
    {
        Damage dmg = aurea.GetDamage();
        dmg.physicalDamage *= damageMultiplier;
        aurea.TakeDamage(dmg);
        if (roundsLeft <= 0)
            Kill();
    }

    public void Kill()
    {
        if (aurea)
            aurea.GetPlayer().StartedTurn -= NewTurn;
        DestroyImmediate(this);
    }
}
