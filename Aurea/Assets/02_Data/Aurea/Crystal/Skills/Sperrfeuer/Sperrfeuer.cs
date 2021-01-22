using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Sperrfeuer", menuName = "Skills/Crystal/Sperrfeuer")]
public class Sperrfeuer : Skill
{
    [SerializeField]
    int amountOfAddedSplitter = 9;

    [SerializeField]
    int lifePointsLeft = 1;

    public override void Use(Damage dmg)
    {
        if (dmg.sender.GetLifePointsLeft() == lifePointsLeft) {
            dmg.sender.SetLifePointsLeft(0);
            dmg.sender.Die();
        }
        else
            dmg.sender.SetLifePointsLeft(lifePointsLeft);

        Kristallisiert kristallisiert = dmg.sender.gameObject.GetComponent<Kristallisiert>();

        if (kristallisiert)
        {
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
