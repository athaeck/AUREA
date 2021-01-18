using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightController))]
public class ParticleVisualizer : MonoBehaviour
{
    FightController fightController = null;

    [SerializeField]
    ParticleSystem selectedParticles = null;

    [SerializeField]
    ParticleSystem targetParticles = null;

    [SerializeField]
    Dictionary<Aurea, ParticleSystem> instantiatedParticles = new Dictionary<Aurea, ParticleSystem>();

    private void Start()
    {
        fightController = GetComponent<FightController>();
        fightController.GameLoaded += AddEventListener;
    }

    private void AddEventListener()
    {
        foreach (Aurea aurea in fightController.GetPlayer().GetAureas())
        {
            aurea.Selected += Selected;
            aurea.ChangedTargets += ChangedTargets;
            aurea.SkillCancled += Cancel;
        }
    }

    private void Selected(Aurea _aurea)
    {
        ParticleSystem newSelected = Instantiate(selectedParticles, _aurea.transform.position, Quaternion.identity);
        instantiatedParticles.Add(_aurea, newSelected);
    }

    private void ChangedTargets(List<Aurea> _aureas)
    {
        foreach (Aurea aurea in _aureas)
        {
            if (!instantiatedParticles.ContainsKey(aurea))
            {
                ParticleSystem newTarget = Instantiate(targetParticles, aurea.transform.position, Quaternion.identity);
                instantiatedParticles.Add(aurea, newTarget);
            }
        }
    }

    private void Cancel()
    {
        foreach (KeyValuePair<Aurea, ParticleSystem> keyValue in instantiatedParticles)
        {
            Destroy(keyValue.Value);
        }
        instantiatedParticles = new Dictionary<Aurea, ParticleSystem>();
    }
}
