using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KristallangriffController : SkillController
{
    bool isInUse = false;
    bool hitTarget = false;
    public float attackDelay = 2f;

    public void FixedUpdate()
    {
    }

    public void TakeInformations(Damage dmg)
    {
        this.GetComponentInChildren<Renderer>().enabled = false;
        information = dmg;
        base.StartAttack();
        if(Player.Instance.AnimationsOn())
            StartCoroutine(WaitTillAttack());
        else {
            base.EndAttack();
            information.targets[0].TakeDamage(information);
            Destroy(this.gameObject);
        }
    }

    bool CheckIfHitTarget()
    {
        return (information.targets[0].transform.position - transform.position).magnitude < 2f;
    }
    IEnumerator WaitTillAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        this.GetComponentInChildren<Renderer>().enabled = true;
        isInUse = true;
    }
}