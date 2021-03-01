using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Kristallangriff", menuName = "Skills/Crystal/Kristallangriff")]
public class Kristallangriff : Skill
{
    [SerializeField]
    private float damageMultiplier = 1.3f;

    [SerializeField]
    private float attackDelay = 2f;

    [SerializeField]
    private AttackAnimationController animation = null;

    public override void Use(Damage _dmg)
    {
        if (_dmg.targets[0].GetMagicalDefence() < _dmg.targets[0].GetPhysicalDefence())
        {
            _dmg.magicalDamage = _dmg.physicalDamage * damageMultiplier;
            _dmg.skillType = SkillType.MAGICAL;
        }
        else
        {
            _dmg.physicalDamage *= damageMultiplier;
            _dmg.skillType = SkillType.PHYSICAL;
        }

        _dmg.attackDelay = attackDelay;

        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation(_dmg);


        foreach (Aurea target in _dmg.targets)
        {
            Damage dmg = _dmg.Copy();
            target.TakeDamage(dmg);
        }
    }

    public override bool IsTargetValid(Aurea _aurea, Aurea _sender)
    {
        if (_aurea == _sender)
            return false;

        // if (_aurea.GetPlayer() == _sender.GetPlayer())
        //     return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        if (_targets.Count > 0 && IsTargetValid(_targets[0], _sender))
            return true;

        return false;
    }
}
