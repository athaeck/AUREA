using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public Aurea sender = null;
    public Aurea target = null;
    public float physicalDamage = 0;
    public float magicalDamage = 0;
    public List<Modifier> modifier = new List<Modifier>();
    public Skill skill;
}
