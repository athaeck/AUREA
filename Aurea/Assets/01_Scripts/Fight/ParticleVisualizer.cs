using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightController))]
public class ParticleVisualizer : MonoBehaviour
{
    FightController fightController = null;

    [SerializeField]
    GameObject selectedParticles = null;

    [SerializeField]
    GameObject targetParticles = null;

    [SerializeField]
    Dictionary<Aurea, GameObject> instantiatedParticles = new Dictionary<Aurea, GameObject>();

    [SerializeField]
    float killInTime = 2f;

    private void Awake()
    {
        fightController = GetComponent<FightController>();
        fightController.GameLoaded += AddEventListener;
    }

    private void AddEventListener()
    {
        foreach (PlayerController player in fightController.players)
        {
            foreach (Aurea aurea in player.GetAureas())
            {
                aurea.Selected += Selected;
                aurea.ChangedTargets += ChangedTargets;
                aurea.SkillCancled += Cancel;
            }
        }
    }

    private void Selected(Aurea _aurea)
    {
        GameObject newSelected = Instantiate(selectedParticles, _aurea.transform.parent);
        if (instantiatedParticles.ContainsKey(_aurea))
            return;
        instantiatedParticles.Add(_aurea, newSelected);
    }

    private void ChangedTargets(List<Aurea> _aureas)
    {
        foreach (Aurea aurea in _aureas)
        {
            if (!instantiatedParticles.ContainsKey(aurea))
            {
                GameObject newTarget = Instantiate(targetParticles, aurea.transform.parent);
                instantiatedParticles.Add(aurea, newTarget);
            }
        }
    }

    private void Cancel()
    {
        if (Player.Instance.animationsOn)
        {
            List<RuneAnimations> objects = new List<RuneAnimations>();
            foreach (KeyValuePair<Aurea, GameObject> keyValue in instantiatedParticles)
            {
                if (keyValue.Value != null)
                {
                    RuneAnimations runeAnimtaions = keyValue.Value.GetComponent<RuneAnimations>();
                    objects.Add(runeAnimtaions);
                }
            }

            StartCoroutine(CancelIn(objects, killInTime));
        }
        else
        {
            foreach (KeyValuePair<Aurea, GameObject> keyValue in instantiatedParticles)
            {
                RuneAnimations runeAnimations = keyValue.Value.GetComponent<RuneAnimations>();
                runeAnimations.turnOff = true;
                runeAnimations.alpha = 0;
            }
        }
        instantiatedParticles = new Dictionary<Aurea, GameObject>();
    }

    IEnumerator CancelIn(List<RuneAnimations> _animations, float _time)
    {
        yield return new WaitForSeconds(_time);

        foreach (RuneAnimations rune in _animations)
        {
            rune.turnOff = true;
        }
    }
}
