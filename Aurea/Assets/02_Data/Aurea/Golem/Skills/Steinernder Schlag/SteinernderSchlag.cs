using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SteinernderSchlag", menuName = "Data/Golem/SteinernderSchlag")]
public class SteinernderSchlag : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    [SerializeField]
    private GameObject attackPrefab = null;

    public override void Use(Damage dmg)
    {
        dmg.physicalDamage *= physicalDamageMultiplier;
        GameObject attack = Instantiate(attackPrefab, dmg.sender.transform);
        SteinernderSchlagController controller = attack.GetComponent<SteinernderSchlagController>();
        controller.TakeInformations(dmg);
    }

    public override bool IsUsable(Damage dmg)
    {
        if (dmg.sender == dmg.targets[0])
            return false;

        if (dmg.sender.GetPlayer() == dmg.targets[0].GetPlayer())
            return false;

        return true;
    }
}
