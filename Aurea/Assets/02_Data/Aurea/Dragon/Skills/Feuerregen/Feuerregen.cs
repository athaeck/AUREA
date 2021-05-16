using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feuerregen", menuName = "Skills/Dragon/Feuerregen")]
public class Feuerregen : Skill
{
    [SerializeField]
    private float magicDamageMultiplier = 1f;

    public override void Use(Damage _dmg)
    {
        _dmg.magicalDamage *= magicDamageMultiplier;
        _dmg.attackDelay = attackDelay;

        List<Aurea> enemyAurea = new List<Aurea>(GetEnemyAurea(_dmg));

        foreach (Aurea aurea in enemyAurea)
        {
            if (aurea != null)
                aurea.TakeDamage(_dmg.Copy());
        }
    }
    public override bool IsTargetValid(Aurea _target, Aurea _sender)
    {
        if (_target.GetPlayer() == _sender.GetPlayer())
            return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        return true;
    }
}
