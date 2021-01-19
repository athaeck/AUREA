using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackAnimationController : ScriptableObject
{
    public abstract void StartAnimation(Damage _dmg);
}
