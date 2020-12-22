using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Steinwurf", menuName = "Data/Golem/Steinwurf")]
public class Steinwurf : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    [SerializeField]
    private GameObject attackPrefab = null;

    public override void Use(Damage dmg)
    {
        dmg.physicalDamage *= physicalDamageMultiplier;
        GameObject attack = Instantiate(attackPrefab, dmg.sender.transform);
        SteinwurfController controller = attack.GetComponent<SteinwurfController>();
        controller.TakeInformations(dmg);
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
