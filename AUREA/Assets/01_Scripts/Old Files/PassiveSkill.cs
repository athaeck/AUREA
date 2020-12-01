using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkill : MonoBehaviour
{
    [SerializeField]
    private List<Modifier> modifier = new List<Modifier>();

    public List<Modifier> Init()
    {
        //TODO
        return modifier;
    }
}
