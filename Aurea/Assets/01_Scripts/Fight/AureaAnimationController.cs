using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AureaAnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Aurea aurea = null;
    private void Start()
    {
        if(IslandController.Instance.activeIsland != Island.ChickenFight) return;
        if (IslandController.Instance.fight.training) return;

        anim = GetComponent<Animator>();
        aurea = GetComponent<Aurea>();
        aurea.StartAttack += Attack;
        //aurea.GetPlayer().HasWon += Won;
        aurea.GotHit += GotHit;
    }

    public void Attack(Damage dmg)
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
