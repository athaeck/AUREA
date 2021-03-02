using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Alptraum", menuName = "Skills/Inkubus/Alptraum")]
public class Alptraum : Skill
{
    [SerializeField]
    private string sleepingSkill = "Sleeping";

    [SerializeField]
    private string amnesiaSkill = "HealthSleeping";

    [SerializeField]
    private float healMultiplier = 0.05f;

    [SerializeField]
    private float damageMultiplier = 5f;

    public override void Use(Damage _dmg)
    {
        List<Aurea> enemyAurea = GetEnemyAurea(_dmg);
        List<Aurea> playerAurea = _dmg.sender.GetPlayer().GetAureas();

        foreach (Aurea enemy in enemyAurea)
        {
            System.Type modifierScript = System.Type.GetType(sleepingSkill);
            Component component = enemy.gameObject.GetComponent(modifierScript);

            if (component) {
                Damage dmg = _dmg.Copy();
                dmg.magicalDamage *= damageMultiplier;
                enemy.TakeDamage(dmg);
            }
        }

        foreach (Aurea aurea in playerAurea)
        {
            System.Type modifierScript = System.Type.GetType(sleepingSkill);
            Component component = aurea.gameObject.GetComponent(modifierScript);

            if (component) {
                aurea.RemoveLifePoints(-(aurea.GetLifePointsMax() * healMultiplier));
                component.SendMessage("Kill");
            }

            System.Type amnesiaModifier = System.Type.GetType(amnesiaSkill);
            Component amnesiaComponent = aurea.gameObject.GetComponent(amnesiaModifier);

            if (amnesiaComponent) {
                aurea.RemoveLifePoints(-(aurea.GetLifePointsMax() * healMultiplier));
                amnesiaComponent.SendMessage("Kill");
            }
        }
    }

    


    public override bool IsTargetValid(Aurea _aurea, Aurea _sender)
    {
        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        // if (_targets.Count > 0 && IsTargetValid(_targets[0], _sender))
        //     return true;

        return true;
    }
}