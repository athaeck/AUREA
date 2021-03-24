using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Schild : MonoBehaviour
{
    Aurea aurea = null;
    float shieldMultiplier = 0.2f;

    [SerializeField]
    float shieldAmount = 0;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        shieldAmount = aurea.GetLifePointsLeft() * shieldMultiplier;
        aurea.BeforeGettingHit += Shield;
    }

    void Shield(Damage _dmg)
    {
        switch (_dmg.skillType)
        {
            case SkillType.MAGICAL:
                if (_dmg.magicalDamage >= shieldAmount)
                {
                    _dmg.magicalDamage -= shieldAmount;
                    shieldAmount = 0;
                }
                else
                {
                    shieldAmount -= _dmg.magicalDamage;
                    _dmg.magicalDamage = 0;
                }
                break;
            default:
                if (_dmg.physicalDamage >= shieldAmount)
                {
                    _dmg.physicalDamage -= shieldAmount;
                    shieldAmount = 0;
                }
                else
                {
                    shieldAmount -= _dmg.physicalDamage;
                    _dmg.magicalDamage = 0;
                }
                break;
        }

        if (shieldAmount <= 0)
            Kill();
    }

    public void Kill()
    {
        if (aurea)
            aurea.BeforeGettingHit -= Shield;
        DestroyImmediate(this);
    }
}
