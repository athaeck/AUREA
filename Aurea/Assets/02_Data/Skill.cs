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
    private string NAME = "";

    [SerializeField]
    private string DESCRIPTION = "";

    [SerializeField]
    private string ID = "";

    [SerializeField]
    private int costs = 0;

    [SerializeField]
    private SkillType skillType = SkillType.PHYSICAL;

    [SerializeField]
    private List<Modifier> modifier = new List<Modifier>();

    public abstract void Use(Damage dmg);
    public abstract bool IsUsable(Damage dmg);
    public string GetName() { return NAME; }
    public string GetDescription() { return DESCRIPTION; }
    public string GetID() { return ID; }
    public int GetCosts() { return costs; }
    public SkillType GetSkillType() { return skillType; }
    public List<Modifier> GetModifier() { return modifier; }

}
