using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Schleier : MonoBehaviour
{
    Aurea aurea = null;
    public float lifepointsLeft = 0;
    float startMultiplier = 0.15f;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        lifepointsLeft = aurea.GetLifePointsLeft() * startMultiplier;
        aurea.BeforeGettingHit += CheckGettingHit;
    }

    void CheckGettingHit(Damage _dmg)
    {

        switch (_dmg.skillType)
        {
            case SkillType.MAGICAL:
                if (_dmg.magicalDamage >= lifepointsLeft)
                {
                    _dmg.magicalDamage -= lifepointsLeft;
                    lifepointsLeft = 0;
                }
                else
                {
                    lifepointsLeft -= _dmg.magicalDamage;
                    _dmg.magicalDamage = 0;
                }
                break;
            default:
                if (_dmg.physicalDamage >= lifepointsLeft)
                {
                    _dmg.physicalDamage -= lifepointsLeft;
                    lifepointsLeft = 0;
                }
                else
                {
                    lifepointsLeft -= _dmg.physicalDamage;
                    _dmg.physicalDamage = 0;
                }
                break;
        }

        if (lifepointsLeft <= 0)
            Kill();
    }

    public void Kill()
    {
        if (aurea)
            aurea.BeforeGettingHit -= CheckGettingHit;
        DestroyImmediate(this);
    }
}
