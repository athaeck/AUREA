using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Transfusion", menuName = "Skills/Illusian/Transfusion")]
public class Transfusion : Skill
{
    [SerializeField]
    private float magicDamageMultiplier = 1.5f;

    [SerializeField]
    private float healthMultiplier = 0.5f;

    [SerializeField]
    private float attackDelay = 2f;

    [SerializeField]
    private AttackAnimationController animation = null;

    public override void Use(Damage _dmg) {
        _dmg.magicalDamage *= magicDamageMultiplier;
        _dmg.attackDelay = attackDelay;

        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation(_dmg);


        foreach (Aurea target in _dmg.targets)
        {
            Damage dmg = _dmg.Copy();
            target.TakeDamage(dmg);
        }

        _dmg.sender.RemoveLifePoints(-_dmg.magicalDamage * healthMultiplier);
    }
    public override bool IsTargetValid(Aurea _target, Aurea _sender) {
        if (_target == _sender)
            return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender) {
        if (_targets.Count > 0 && IsTargetValid(_targets[0], _sender))
            return true;

        return false;
    }
}
