using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public abstract void Use(Damage dmg);
    public abstract bool IsTargetValid(Aurea _aurea, Aurea _sender);
    public abstract bool CheckTargets(List<Aurea> _targets, Aurea _sender);
    public string GetName() { return NAME; }
    public string GetDescription() { return DESCRIPTION; }
    public string GetID() { return ID; }
    public int GetCosts() { return costs; }
    public SkillType GetSkillType() { return skillType; }
    public List<string> GetModifier() { return modifier; }

}
