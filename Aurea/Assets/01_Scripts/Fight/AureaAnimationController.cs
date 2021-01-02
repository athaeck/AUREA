using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Aurea))]
public class AureaAnimationController : MonoBehaviour
{
    Animator anim = null;
    Aurea aurea = null;
    private void Start()
    {
        anim = GetComponent<Animator>();
        aurea.StartAttack += Attack;
        aurea.GetPlayer().HasWon += Won;
        aurea.GotHit += GotHit;
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void GotHit()
    {
        anim.SetTrigger("GotHit");
    }

    public void Won()
    {
        if (aurea.IsAlive())
            anim.SetTrigger("Won");
    }
}
