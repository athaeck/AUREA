using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeroList", menuName = "Data/HeroList")]
public class AureaList : ScriptableObject
{
    public List<AureaData> aureas = new List<AureaData>();
}
