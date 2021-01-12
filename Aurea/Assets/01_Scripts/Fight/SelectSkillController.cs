using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSkillController : MonoBehaviour
{
    [SerializeField]
    private PlayerController player = null;

    [SerializeField]
    private Aurea selectedAurea = null;

    [SerializeField]
    private GameObject selectedSkillCanvas = null;

    [SerializeField]
    private Text firstSkillText = null;

    [SerializeField]
    private Text secondSkillText = null;

    [SerializeField]
    private Text thirdSkillText = null;

    [SerializeField]
    private Text forthSkillText = null;

    private void Start()
    {
        player.SelectedAurea += InitView;
        player.ResetedSelection += ResetView;
    }

    private void ResetView()
    {
        selectedSkillCanvas.SetActive(false);
    }

    void InitView(Aurea _aurea)
    {
        selectedSkillCanvas.SetActive(true);
        selectedAurea = _aurea;
        List<Skill> skills = _aurea.GetSkills();

        firstSkillText.text = skills[0].GetName();
        secondSkillText.text = skills[1].GetName();
        thirdSkillText.text = skills[2].GetName();
        forthSkillText.text = skills[3].GetName();
    }

    public void UseSkill(int i)
    {
        selectedAurea.activeSkill = selectedAurea.GetSkills()[i];
        ResetView();
    }
}
