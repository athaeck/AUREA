using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillType
{
    PHYSICAL,
    MAGICAL
}

public abstract class Skill : ScriptableObject
{

    [SerializeField]
    protected string NAME = "";

    [SerializeField]
    protected string DESCRIPTION = "";

    [SerializeField]
    protected string ID = "";

    [SerializeField]
    protected int costs = 0;

    [SerializeField]
    protected SkillType skillType = SkillType.PHYSICAL;

    [SerializeField]
    protected List<string> modifier = new List<string>();

    [SerializeField]
    protected AttackAnimationController animation = null;

    [SerializeField]
    protected float attackDelay = 1.8f;

    public abstract void Use(Damage _dmg);
    public abstract bool IsTargetValid(Aurea _aurea, Aurea _sender);
    public abstract bool CheckTargets(List<Aurea> _targets, Aurea _sender);
    public string GetName() { return NAME; }
    public string GetDescription() { return DESCRIPTION; }
    public string GetID() { return ID; }
    public int GetCosts() { return costs; }
    public SkillType GetSkillType() { return skillType; }
    public List<string> GetModifier() { return modifier; }

    protected List<Aurea> GetEnemyAurea(Damage _dmg)
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
}
