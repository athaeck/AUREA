using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(MeshRenderer))]
public class RuneAnimations : MonoBehaviour
{
    Renderer ren = null;

    [SerializeField]
    VisualEffect effect = null;

    [SerializeField]
    float turnOnSpeed = 2f;

    [SerializeField]
    float turnOutSpeed = 0.01f;

    [SerializeField]
    private float waitTillDie = 5f;

    public bool turnOff = false;
    public float alpha = 0;

    bool waitToDie = false;
    void Start()
    {
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitToDie) return;

        if (!turnOff && alpha <= 1)
        {
            alpha += turnOnSpeed * Time.deltaTime;
            ren.material.SetFloat("Vector1_3B7026CA", alpha);
        }
        else if (turnOff)
        {
            effect.SetInt("spawnAmount", 0);
            alpha -= turnOnSpeed * Time.deltaTime;
            ren.material.SetFloat("Vector1_3B7026CA", alpha);

            if (alpha <= 0)
            {
                if (Player.Instance.animationsOn)
                {
                    waitToDie = true;
                    StartCoroutine(WaitTillDie());
                }
                else
                    Destroy(this.gameObject);
            }
        }
    }

    IEnumerator WaitTillDie()
    {
        yield return new WaitForSeconds(waitTillDie);
        Destroy(this.gameObject);
    }
}
