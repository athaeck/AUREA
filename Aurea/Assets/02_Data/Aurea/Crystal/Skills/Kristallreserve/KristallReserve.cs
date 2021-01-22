using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KristallReserve", menuName = "Skills/Crystal/KristallReserve")]
public class KristallReserve : Skill
{
    [SerializeField]
    int amountOfAddedSplitter = 1;

    public override void Use(Damage dmg)
    {
        Kristallisiert kristallisiert = dmg.sender.gameObject.GetComponent<Kristallisiert>();

        if(kristallisiert) {
            kristallisiert.AddSplitter(amountOfAddedSplitter);
        }
        // dmg.physicalDamage *= physicalDamageMultiplier;
        // GameObject attack = Instantiate(attackPrefab, dmg.sender.transform);
        // ErdbebenController controller = attack.GetComponent<ErdbebenController>();
        // controller.TakeInformations(dmg);
    }

    public override bool IsTargetValid(Aurea _aurea, Aurea _sender)
    {
        return true;
    }

    public override bool CheckTargets(List<Aurea> _targets, Aurea _sender)
    {
        return true;
    }
}
