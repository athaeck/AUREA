using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CrystalVisualizationController : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> crystalParticles = new List<ParticleSystem>();

    private PlayerController player = null;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        ActiveCrystals(player.GetAPLeft());
        player.ChangedAP += ActiveCrystals;
    }
    
    public void ActiveCrystals(int num)
    {
        for (int i = 0; i < crystalParticles.Count; i++)
        {
            if (i < num)
                crystalParticles[i].gameObject.SetActive(true);
            else
                crystalParticles[i].gameObject.SetActive(false);
        }
    }
}
