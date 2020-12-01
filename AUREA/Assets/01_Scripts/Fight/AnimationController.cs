using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    Animator anim = null;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Walk(bool walk)
    {
        anim.SetBool("Walk", walk);
    }

    public void Cast() {
        anim.SetTrigger("Cast");
    }
}
