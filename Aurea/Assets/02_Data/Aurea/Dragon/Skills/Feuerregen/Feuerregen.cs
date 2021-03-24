using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feuerregen", menuName = "Skills/Dragon/Feuerregen")]
public class Feuerregen : Skill
{
    [SerializeField]
    private float magicDamageMultiplier = 1f;

    [SerializeField]
    private int amountOfFireballs = 5;

    public override void Use(Damage _dmg)
    {
        _dmg.physicalDamage *= magicDamageMultiplier;
        _dmg.attackDelay = attackDelay;

        List<Aurea> enemyAurea = new List<Aurea>(GetEnemyAurea(_dmg));

        for (int i = 0; i < amountOfFireballs; i++)
        {
            int rndAurea = Random.Range(0, enemyAurea.Count);

            Damage dmg = _dmg.Copy();
            dmg.targets = new List<Aurea>();
            dmg.targets.Add(enemyAurea[rndAurea]);


            if (Player.Instance.AnimationsOn() && animation)
                animation.StartAnimation(dmg);
            else
            {
                if (dmg.targets[0] != null)
                    dmg.targets[0].TakeDamage(dmg);
            }
        }
    }
    public override bool IsTargetValid(Aurea _target, Aurea _sender)
    {
        if (_target.GetPlayer() == _sender.GetPlayer())
            return false;

        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        return true;
    }
}
