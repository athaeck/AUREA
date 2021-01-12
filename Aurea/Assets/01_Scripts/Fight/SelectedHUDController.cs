using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FollowTarget))]
public class SelectedHUDController : MonoBehaviour
{
    [SerializeField]
    FollowTarget follow = null;

    [SerializeField]
    private Text firstSkillButton = null;

    [SerializeField]
    private Text secondSkillButton = null;

    [SerializeField]
    private Text thirdSkillButton = null;

    [SerializeField]
    private Text fourthSkillButton = null;

    [SerializeField]
    private Slider healthSlider = null;

    Aurea selected = null;

    public void TakeTarget(Aurea aurea)
    {
        if (selected)
            selected.ChangedLifepoints -= UpdateUI;

        selected = aurea;
        selected.ChangedLifepoints += UpdateUI;

        follow.TakeTarget(selected.transform);
        UpdateUI();
    }

    public void UpdateUI()
    {
        List<Skill> skills = selected.GetSkills();

        firstSkillButton.text = skills[0].GetName();
        secondSkillButton.text = skills[1].GetName();
        thirdSkillButton.text = skills[2].GetName();
        fourthSkillButton.text = skills[3].GetName();

        float health = selected.GetLifePointsLeft() / selected.GetLifePointsMax();
        healthSlider.value = health;
    }

    // public void UseSkill(int number)
    // {
    //     Skill skillToUse = selected.GetSkills()[number];
    //     selected.GetPlayer().SelectSkill(skillToUse);
    // }

}
