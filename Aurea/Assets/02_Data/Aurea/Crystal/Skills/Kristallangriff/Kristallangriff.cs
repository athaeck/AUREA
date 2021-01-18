using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Kristallangriff", menuName = "Skills/Crystal/Kristallangriff")]
public class Kristallangriff : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    [SerializeField]
    private float attackDelay = 2f;

    [SerializeField]
    private AttackAnimationController animation = null;

    public override void Use(Damage dmg)
    {
        dmg.modifier = this.modifier;
        dmg.physicalDamage *= physicalDamageMultiplier;
        dmg.attackDelay = attackDelay;

        if (Player.Instance.AnimationsOn() && animation)
            animation.StartAnimation();


        foreach (Aurea target in dmg.targets)
        {
            target.TakeDamage(dmg);
        }
        // GameObject attack = Instantiate(attackPrefab, dmg.sender.transform);
        // KristallangriffController controller = attack.GetComponent<KristallangriffController>();
        // controller.TakeInformations(dmg);
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
