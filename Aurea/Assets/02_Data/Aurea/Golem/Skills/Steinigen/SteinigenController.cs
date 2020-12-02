using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteinigenController : SkillController
{
    bool isInUse = false;
    bool hitTarget = false;

    public float flightSpeed = 5f;
    public float attackDelay = 2f;

    public void FixedUpdate()
    {
        if (!isInUse)
            return;

        transform.position = Vector3.Lerp(transform.position, information.target.transform.position, Time.deltaTime * flightSpeed);

        hitTarget = CheckIfHitTarget();

        if (hitTarget)
        {
            base.EndAttack();
            information.target.TakeDamage(information);
            Destroy(this.gameObject);
        }
    }

    public void TakeInformations(Damage dmg)
    {
        dmg.sender.GetComponent<AnimationController>().Attack();
        this.GetComponentInChildren<Renderer>().enabled = false;
        information = dmg;
        base.StartAttack();
        StartCoroutine(WaitTillAttack());
    }

    bool CheckIfHitTarget()
    {
        return (information.target.transform.position - transform.position).magnitude < 2f;
    }
    IEnumerator WaitTillAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        this.GetComponentInChildren<Renderer>().enabled = true;
        isInUse = true;
    }
}