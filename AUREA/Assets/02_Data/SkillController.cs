using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    protected Damage information = null;
    public void StartAttack()
    {
        information.sender.GetPlayer().GetGameController().StartUsingSkill();
    }

    public void EndAttack()
    {
        information.sender.GetPlayer().GetGameController().EndUsingSkill();
    }
}
