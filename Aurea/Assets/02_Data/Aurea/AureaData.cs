using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Aurea", menuName = "Data/Aurea")]
[Serializable]
public class AureaData : ScriptableObject
{
    public string NAME;
    public string ID;
    public string DESCRIPTION;
    public List<AureaLevel> levels = new List<AureaLevel>();
}
