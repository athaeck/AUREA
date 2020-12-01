using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteinigenMiniController : MonoBehaviour
{
    public float lifetimeMin = 3f;
    public float lifeTimeMax = 6f;
    float lifetime = 0f;
    float timeSinceStart = 0f;

    private void Start()
    {
        lifetime = Random.Range(lifetimeMin, lifeTimeMax);
    }
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        
        if (timeSinceStart >= lifetime)
            Destroy(this.gameObject);
    }
}
