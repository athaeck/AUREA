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
    private float damage = 5f;

    [SerializeField]
    private AttackAnimationController animation = null;

    public override void Use(Damage _dmg)
    {
        List<Aurea> enemyAurea = GetEnemyAurea(_dmg);
        List<Aurea> playerAurea = _dmg.sender.GetPlayer().GetAureas();

        foreach (Aurea enemy in enemyAurea)
        {
            System.Type modifierScript = System.Type.GetType(sleepingSkill);
            Component component = enemy.gameObject.GetComponent(modifierScript);

            if (component)
                enemy.TakeDamage(_dmg.sender.GetDamage());
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

    List<Aurea> GetEnemyAurea(Damage _dmg)
    {
        PlayerController controllerPlayer = IslandController.Instance.fight.GetPlayer();
        PlayerController controllerEnemy = IslandController.Instance.fight.GetEnemy();

        foreach (Aurea _aurea in controllerPlayer.GetAureas())
        {
            if (_aurea == _dmg.sender)
                return controllerEnemy.GetAureas();
        }

        foreach (Aurea _aurea in controllerEnemy.GetAureas())
        {
            if (_aurea == _dmg.sender)
                return controllerPlayer.GetAureas();
        }

        return new List<Aurea>();
    }


    public override bool IsTargetValid(Aurea _aurea, Aurea _sender)
    {
        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        return true;
    }
}