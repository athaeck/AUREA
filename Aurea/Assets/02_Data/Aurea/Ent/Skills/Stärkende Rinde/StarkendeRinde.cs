using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StarkendeRinde", menuName = "Skills/Ent/StarkendeRinde")]
public class StarkendeRinde : Skill
{
    public override void Use(Damage _dmg) {
        
        _dmg.physicalDamage = 0;
        _dmg.magicalDamage = 0;
        _dmg.attackDelay = attackDelay;

        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation(_dmg);

        foreach (Aurea target in _dmg.targets)
        {
            Damage dmg = _dmg.Copy();
            target.TakeDamage(dmg);
        }
    }
    public override bool IsTargetValid(Aurea _target, Aurea _sender) {
        // if (_target.GetPlayer() == _sender.GetPlayer())
        //     return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender) {
        if (_targets.Count > 0 && IsTargetValid(_targets[0], _sender))
            return true;

        return false;
    }
}