using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kraftschlaf", menuName = "Skills/Inkubus/Kraftschlaf")]
public class Kraftschlaf : Skill
{
    [SerializeField]
    private string amnesiaSkill = "HealthSleeping";

    [SerializeField]
    private float healthMultiplier = 0.5f;
    
    public override void Use(Damage _dmg)
    {
        System.Type modifierScript = System.Type.GetType(amnesiaSkill);
        Component component = _dmg.targets[0].gameObject.GetComponent(modifierScript);

        if (component)
            return;
        
        _dmg.targets[0].RemoveLifePoints(-(_dmg.targets[0].GetLifePointsMax() * healthMultiplier));
        _dmg.targets[0].gameObject.AddComponent(modifierScript);
    }

    public override bool IsTargetValid(Aurea _aurea, Aurea _sender)
    {
        if (_aurea.GetPlayer() != _sender.GetPlayer())
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