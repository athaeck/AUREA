using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteinigenController : SkillController
{
    public float spawnHeight = 5f;
    public GameObject afterHitPrefab = null;
    public int numAfterHits = 10;
    public float afterHitRange = 3f;

    public void TakeInformations(Damage dmg)
    {
        information = dmg;
        dmg.sender.GetComponent<AnimationController>().Cast();
        transform.position = information.target.transform.position + Vector3.up * spawnHeight;
        base.StartAttack();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform == information.target.transform)
        {
            if(afterHitPrefab) {
                for(int i = 0; i < numAfterHits; i++) {
                    Vector3 spawnPos = transform.position + Vector3.up;
                    spawnPos.x += Random.Range(-afterHitRange, afterHitRange);
                    spawnPos.z += Random.Range(-afterHitRange, afterHitRange);

                    Instantiate(afterHitPrefab, spawnPos, Random.rotation);
                }
            }
            base.EndAttack();
            information.target.TakeDamage(information);
            Destroy(this.gameObject);

        }
    }
}