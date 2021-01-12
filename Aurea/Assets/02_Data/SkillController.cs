using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected Damage information = null;
    public void StartAttack()
    {
        IslandController.Instance.fight.StartUsingSkill();
    }

    public void EndAttack()
    {
        IslandController.Instance.fight.EndUsingSkill();
    }
}
