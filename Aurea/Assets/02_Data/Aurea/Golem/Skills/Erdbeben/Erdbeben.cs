using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Erdbeben", menuName = "Data/Golem/Erdbeben")]
public class Erdbeben : Skill
{
    [SerializeField]
    private float physicalDamageMultiplier = 1.3f;

    [SerializeField]
    private GameObject attackPrefab = null;

    //[SerializeField]
    //private float attackDelay = 1.8f;
    public override void Use(Damage dmg)
    {
        dmg.physicalDamage *= physicalDamageMultiplier;
        GameObject attack = Instantiate(attackPrefab, dmg.sender.transform);
        ErdbebenController controller = attack.GetComponent<ErdbebenController>();
        controller.TakeInformations(dmg);
    }

    public override bool IsUsable(Damage dmg)
    {
        if (dmg.sender == dmg.target)
            return false;

        if (dmg.sender.GetPlayer() == dmg.target.GetPlayer())
            return false;

        return true;
    }
}
