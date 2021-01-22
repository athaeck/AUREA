using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KristallSplitter", menuName = "Skills/Crystal/KristallSplitter")]
public class KristallSplitter : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    [SerializeField]
    private float attackDelay = 2f;

    [SerializeField]
    private AttackAnimationController animation = null;

    public override void Use(Damage _dmg)
    {
        _dmg.modifier = this.modifier;
        _dmg.physicalDamage *= physicalDamageMultiplier;
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

        if (_aurea.GetPlayer() == _sender.GetPlayer())
            return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        if (_targets.Count > 1 && IsTargetValid(_targets[0], _sender) && IsTargetValid(_targets[1], _sender))
            return true;

        return false;
    }
}
