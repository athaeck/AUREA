using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DasDunkleSakrament", menuName = "Skills/Illusian/Das dunkle Sakrament")]
public class DunkleSakrament : Skill
{
    public override bool IsTargetValid(Aurea _target, Aurea _sender) {
        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender) {
        return true;
    }

    public override void Use(Damage _dmg) {

    }

}
