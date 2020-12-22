using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErdbebenController : SkillController
{
    public float attackDelay = 2f;

    public void TakeInformations(Damage dmg)
    {
        dmg.sender.GetComponent<AnimationController>().Attack();
        information = dmg;
        base.StartAttack();
        StartCoroutine(WaitTillAttack());
    }
    public void DoDamage()
    {
        base.EndAttack();

        PlayerController player = IslandController.Instance.fight.GetPlayer();
        PlayerController enemy = IslandController.Instance.fight.GetEnemy();
        
        List<Aurea> targets = new List<Aurea>();

        if(player == information.sender) 
            targets = enemy.GetAureas();
        else
            targets = player.GetAureas();

        foreach(Aurea target in targets) {
            if(target.IsAlive())
                target.TakeDamage(information);
        }

        Destroy(this.gameObject);
    }
    IEnumerator WaitTillAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        DoDamage();
    }
}