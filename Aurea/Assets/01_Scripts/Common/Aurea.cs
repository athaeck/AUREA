using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Animator))]
public class Aurea : MonoBehaviour
{
    public Action<Damage> StartAttack;
    public Action ChangedLifepoints;
    public Action<Aurea> Died;
    public Action<Aurea> Selected;
    public Action<List<Aurea>> ChangedTargets;
    public Action SkillCancled;
    public Action GotHit;


    [SerializeField]
    private AureaData data = null;

    [SerializeField]
    public int level = 0;

    [SerializeField]
    private float lifePointsLeft = 0;

    public List<Modifier> modifier = new List<Modifier>();
    public List<ItemData> activeItems = new List<ItemData>();

    private PlayerController player = null;
    private Skill _activeSkill;
    public Skill activeSkill
    {
        get
        {
            return _activeSkill;
        }
        set
        {
            if (value && player.GetAPLeft() >= value.GetCosts())
            {
                _activeSkill = value;
                Selected?.Invoke(this);
            }
            else
                _activeSkill = null;

            if (_activeSkill && _activeSkill.CheckTargets(targets, this))
                UseSkill();
        }
    }

    public List<Aurea> targets = new List<Aurea>();

    public void Init(int initLevel, PlayerController aureaPlayer)
    {
        level = initLevel;
        player = aureaPlayer;
        lifePointsLeft = data.levels[level - 1].lifePoints;
    }

    public bool IsAlive() { return lifePointsLeft > 0; }

    public void TakeTarget(Aurea _aurea)
    {
        if (!activeSkill)
        {
            Debug.LogError("Selected Target but no skill active!");
            return;
        }

        Debug.Log("Took Target and have skill active: " + activeSkill.name);

        foreach (Aurea target in targets)
        {
            if (target == _aurea)
            {
                targets.Remove(_aurea);
                ChangedTargets?.Invoke(targets);

                if (targets.Count <= 0)
                {
                    CancelSkill();
                }
                return;
            }
        }

        if (activeSkill.IsTargetValid(_aurea, this))
        {
            targets.Add(_aurea);
            ChangedTargets?.Invoke(targets);

            if (activeSkill.CheckTargets(targets, this))
                UseSkill();
            else
                CancelSkill();
        }
        else
            CancelSkill();
    }

    private void UseSkill()
    {
        Debug.Log("Using SKill : " + activeSkill.name + "on n targets: " + targets.Count);
        Damage dmg = GetDamage(targets, activeSkill);
        StartAttack?.Invoke(dmg);
        player.RemoveAP(activeSkill.GetCosts());
        activeSkill.Use(dmg);
        CancelSkill();
    }

    public void CancelSkill()
    {
        targets = new List<Aurea>();
        activeSkill = null;
        SkillCancled?.Invoke();
    }
    public void TakeDamage(Damage dmg)
    {
        Debug.Log("Got hit " + this.name);
        switch (dmg.skill.GetSkillType())
        {
            case SkillType.MAGICAL:
                RemoveLifePoints(dmg.magicalDamage - data.levels[level - 1].magicalDamage);
                break;
            case SkillType.PHYSICAL:
            default:
                RemoveLifePoints(dmg.physicalDamage - data.levels[level - 1].physicalDefense);
                break;
        }

        if (lifePointsLeft <= 0)
            Die();
    }

    public void Die()
    {
        Died?.Invoke(this);
    }

    #region Getter, Setter
    public string GetName() { return data.NAME; }
    public string GetID() { return data.ID; }
    public string GetDescription() { return data.DESCRIPTION; }
    public List<Skill> GetSkills() { return data.levels[level - 1].skills; }
    public PlayerController GetPlayer() { return player; }
    public Damage GetDamage(List<Aurea> _targets, Skill _skill)
    {
        Damage newDamage = new Damage
        {
            sender = this,
            targets = _targets,
            skill = _skill,
            physicalDamage = data.levels[level - 1].physicalDamage,
            magicalDamage = data.levels[level - 1].magicalDamage,
            modifier = _skill.GetModifier()
        };

        return newDamage;
    }
    public int GetLevel() { return level; }
    public void SetLevel(int level) { this.level = level; }
    public float GetLifePointsMax() { return data.levels[level - 1].lifePoints; }
    public float GetLifePointsLeft() { return lifePointsLeft; }
    public void SetLifePointsLeft(float amount)
    {
        this.lifePointsLeft = amount;
        ChangedLifepoints?.Invoke();
    }
    public void RemoveLifePoints(float amount)
    {
        this.lifePointsLeft -= amount;
        ChangedLifepoints?.Invoke();
    }

    public float GetMagicalDamage() { return data.levels[level - 1].magicalDamage; }
    public float GetMagicalDefence() { return data.levels[level - 1].magicalDefense; }
    public float GetPhysicalDamage() { return data.levels[level - 1].physicalDamage; }
    public float GetPhysicalDefence() { return data.levels[level - 1].physicalDefense; }
    #endregion
}
