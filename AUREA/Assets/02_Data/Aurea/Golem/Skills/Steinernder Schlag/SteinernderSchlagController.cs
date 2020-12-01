using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SteinernderSchlagState
{
    WalkTowardsTarget,
    HitTarget,
    WalkBackOnPlace,
    RotateToIdentity
}

public class SteinernderSchlagController : SkillController
{
    bool isInUse = false;
    bool startHitting = false;
    bool hitTarget = false;
    SteinernderSchlagState state = SteinernderSchlagState.WalkTowardsTarget;

    public float movementSpeed = 3f;
    public float rotationSpeed = 5f;
    public float attackDistance = 3f;
    public float attackDelay = 2f;
    public float afterAttackDelay = 0.5f;

    public void FixedUpdate()
    {
        if (!isInUse)
            return;

        switch (state)
        {
            case SteinernderSchlagState.WalkTowardsTarget:
                WalkTowardsTarget();
                break;
            case SteinernderSchlagState.HitTarget:
                HitTarget();
                break;
            case SteinernderSchlagState.WalkBackOnPlace:
                WalkBackOnPlace();
                break;
            case SteinernderSchlagState.RotateToIdentity:
                RotateToIdentity();
                break;
            default:
                break;
        }
    }

    private void WalkTowardsTarget()
    {
        Vector3 pos = information.sender.transform.position;
        Vector3 tar = information.target.transform.position;
        Vector3 dir = (tar - pos).normalized;
        information.sender.transform.position += dir * Time.deltaTime * movementSpeed;

        Vector3 targetDirection = (tar - pos).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        information.sender.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        information.sender.GetComponent<AnimationController>().Walk(true);

        if (GetDistanstanceToTarget() < attackDistance)
            state = SteinernderSchlagState.HitTarget;
    }

    private void HitTarget()
    {
        if (startHitting) return;

        information.sender.GetComponent<AnimationController>().Attack();
        StartCoroutine(WaitTillAttack());
        startHitting = true;
    }

    private void WalkBackOnPlace()
    {
        Vector3 pos = information.sender.transform.localPosition;
        Vector3 tar = Vector3.zero;
        Vector3 dir = (tar - pos).normalized;

        information.sender.transform.position += dir * Time.deltaTime * movementSpeed;

        Vector3 targetDirection = (tar - pos).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        information.sender.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        information.sender.GetComponent<AnimationController>().Walk(true);

        if (information.sender.transform.localPosition == Vector3.zero)
            state = SteinernderSchlagState.RotateToIdentity;
    }

    private void RotateToIdentity()
    {
        Quaternion targetRotation = Quaternion.identity;
        information.sender.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        information.sender.GetComponent<AnimationController>().Walk(false);

        if (information.sender.transform.rotation == Quaternion.identity)
            EndSkill();
    }

    private void EndSkill()
    {
        base.EndAttack();
        Destroy(this.gameObject);
    }

    public void TakeInformations(Damage dmg)
    {
        information = dmg;
        isInUse = true;
        base.StartAttack();
    }

    float GetDistanstanceToTarget()
    {
        return (information.target.transform.position - information.sender.transform.position).magnitude;
    }
    IEnumerator WaitTillAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        information.target.TakeDamage(information);
        yield return new WaitForSeconds(afterAttackDelay);
        state = SteinernderSchlagState.WalkBackOnPlace;
    }
}