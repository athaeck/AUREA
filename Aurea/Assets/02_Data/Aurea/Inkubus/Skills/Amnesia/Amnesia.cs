using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Amnesia", menuName = "Skills/Inkubus/Amnesia")]
public class Amnesia : Skill
{
    [SerializeField]
    private string amnesiaSkill = "AmnesiaModifier";

    public override void Use(Damage _dmg)
    {
        System.Type modifierScript = System.Type.GetType(amnesiaSkill);
        Component component = _dmg.targets[0].gameObject.GetComponent(modifierScript);

        if (component)
            component.SendMessage("Kill");

        _dmg.targets[0].gameObject.AddComponent(modifierScript);
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