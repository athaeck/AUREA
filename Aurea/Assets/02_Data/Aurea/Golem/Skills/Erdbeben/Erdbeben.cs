using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Erdbeben", menuName = "Skills/Golem/Erdbeben")]
public class Erdbeben : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    public override void Use(Damage _dmg)
    {
        _dmg.physicalDamage *= physicalDamageMultiplier;

        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation(_dmg);

        List<Aurea> enemys = GetEnemyAurea(_dmg);
        Debug.Log("Here1: " + enemys.Count);

        foreach (Aurea aurea in enemys)
        {
            Debug.Log("Here");
            Debug.Log("Aurea is: " + aurea);
            if (aurea != null)
                aurea.TakeDamage(_dmg.Copy());
        }
    }
    public override bool IsTargetValid(Aurea _aurea, Aurea _sender)
    {
        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        return true;
    }
}
