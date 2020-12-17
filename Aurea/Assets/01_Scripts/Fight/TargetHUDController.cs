using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FollowTarget))]
public class TargetHUDController : MonoBehaviour
{
    [SerializeField]
    FollowTarget follow = null;

    [SerializeField]
    private Slider healthSlider = null;

    Aurea target = null;

    private void Start()
    {
        follow = GetComponent<FollowTarget>();
    }

    public void TakeTarget(Aurea aurea)
    {
        if (target)
            target.ChangedLifepoints -= UpdateUI;

        target = aurea;
        target.ChangedLifepoints += UpdateUI;

        follow.TakeTarget(target.transform);
        UpdateUI();
    }

    public void UpdateUI()
    {
        float health = target.GetLifePointsLeft() / target.GetLifePointsMax();
        healthSlider.value = health;
    }
}
