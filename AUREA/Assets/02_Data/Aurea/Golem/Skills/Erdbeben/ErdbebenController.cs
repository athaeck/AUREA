using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using EZCameraShake;
public class ErdbebenController : SkillController
{
    public float attackDelay = 2f;

    public void TakeInformations(Damage dmg)
    {
        information = dmg;
        base.StartAttack();
        dmg.sender.GetComponent<AnimationController>().Cast();
        //CameraShaker.Instance.ShakeOnce(4f, 4f, 4f, 4f);
        StartCoroutine(WaitTillAttack());
    }
    IEnumerator WaitTillAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        foreach(Aurea aurea in information.target.GetPlayer().GetAureas()) {
            aurea.TakeDamage(information);
        }
        Destroy(this.gameObject);
    }
}