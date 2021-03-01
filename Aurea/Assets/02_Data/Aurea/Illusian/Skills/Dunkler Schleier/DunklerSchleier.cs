using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DunklerSchleier", menuName = "Skills/Illusian/Dunkler Schleier")]
public class DunklerSchleier : Skill
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
