using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "AureaLevel", menuName = "Data/Aurea Level")]
public class AureaLevel : ScriptableObject
{
    public int level;
    public float lifePoints;
    public float physicalDamage;
    public float physicalDefense;
    public float magicalDamage;
    public float magicalDefense;
    public List<Skill> skills;
    public List<Skill> passiveSkills;
    public GameObject prefab;
}
