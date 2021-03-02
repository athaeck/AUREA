using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Steinwurf", menuName = "Skills/Golem/Steinwurf")]
public class Steinwurf : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    public override void Use(Damage _dmg)
    {
        _dmg.physicalDamage *= physicalDamageMultiplier;

        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation(_dmg);

        foreach (Aurea aurea in _dmg.targets)
        {
            aurea.TakeDamage(_dmg.Copy());
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
        if (_targets.Count > 0 && IsTargetValid(_targets[0], _sender))
            return true;

        return false;
    }
}
