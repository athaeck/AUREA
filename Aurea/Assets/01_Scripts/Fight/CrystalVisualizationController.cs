using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalVisualizationController : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> crystalParticles = new List<ParticleSystem>();

    public void ActiveCrystals(int num)
    {
        for(int i = 0; i < crystalParticles.Count; i++)
        {
            if (i < num)
                crystalParticles[i].gameObject.SetActive(true);
            else
                crystalParticles[i].gameObject.SetActive(false);
        }
    }
}
