using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(MovementController))]
public class CharacterAnimationController : MonoBehaviour
{
    CharacterController controller = null;
    Animator animator = null;
    MovementController movement = null;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movement = GetComponent<MovementController>();
    }
    private void Update()
    {
        float speed = Remap(controller.velocity.magnitude, 0, movement.GetSpeed(), 0, 1);
        animator.SetFloat("Speed", speed);
    }
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
