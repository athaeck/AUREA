using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AureaList", menuName = "Data/AureaList")]
public class AureaList : ScriptableObject
{
    public List<AureaData> aureas = new List<AureaData>();
}
