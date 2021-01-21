using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public Aurea sender = null;
    public List<Aurea> targets = null;
    public float physicalDamage = 0;
    public float magicalDamage = 0;
    public List<string> modifier = new List<string>();
    public float attackDelay = 0;
    public Skill skill;

    public Damage Copy() {
        Damage copy = new Damage();
        copy.sender = sender;
        copy.targets = targets;
        copy.physicalDamage = physicalDamage;
        copy.magicalDamage = magicalDamage;
        copy.modifier = modifier;
        copy.attackDelay = attackDelay;
        copy.skill = skill;

        return copy;
    }
}
