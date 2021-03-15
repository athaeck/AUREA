using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DasDunkleSakrament", menuName = "Skills/Illusian/Das dunkle Sakrament")]
public class DunkleSakrament : Skill
{
    public override void Use(Damage _dmg)
    {
        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation(_dmg);

        for(int i = 0; i < _dmg.targets.Count; i++) {
            Damage dmg = _dmg.Copy();
            dmg.magicalDamage = (1 / dmg.targets[i].GetMagicalDefence()) * dmg.targets[i].GetLifePointsLeft();
            dmg.targets[i].TakeDamage(dmg);
        }
    }
    public override bool IsTargetValid(Aurea _target, Aurea _sender) {
        if (_target.GetPlayer() == _sender.GetPlayer())
            return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender) {
        if (_targets.Count > 0 && IsTargetValid(_targets[0], _sender))
            return true;

        return false;
    }


}
