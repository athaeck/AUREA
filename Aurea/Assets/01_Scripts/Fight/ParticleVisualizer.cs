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
        foreach (Aurea aurea in fightController.GetPlayer().GetAureas())
        {
            aurea.Selected += Selected;
            aurea.ChangedTargets += ChangedTargets;
            aurea.SkillCancled += Cancel;
        }
    }

    private void Selected(Aurea _aurea)
    {
        GameObject newSelected = Instantiate(selectedParticles, _aurea.transform.parent);
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
            List<GameObject> objects = new List<GameObject>();
            foreach (KeyValuePair<Aurea, GameObject> keyValue in instantiatedParticles)
            {
                objects.Add(keyValue.Value);
            }
            StartCoroutine(CancelIn(objects, killInTime));
        }
        else
        {
            foreach (KeyValuePair<Aurea, GameObject> keyValue in instantiatedParticles)
            {
                Destroy(keyValue.Value);
            }
        }
        instantiatedParticles = new Dictionary<Aurea, GameObject>();
    }

    IEnumerator CancelIn(List<GameObject> _particles, float _time)
    {
        yield return new WaitForSeconds(_time);

        foreach (GameObject _obj in _particles)
        {
            Destroy(_obj);
        }
    }
}
