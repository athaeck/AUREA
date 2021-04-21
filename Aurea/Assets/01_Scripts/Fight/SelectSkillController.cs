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
    private Text firstSkillDescription = null;

    [SerializeField]
    private Text firstSkillCosts = null;

    [SerializeField]
    private Text secondSkillText = null;
    [SerializeField]
    private Text secondSkillDescription = null;

    [SerializeField]
    private Text secondSkillCosts = null;

    [SerializeField]
    private Text thirdSkillText = null;
    [SerializeField]
    private Text thirdSkillDescription = null;

    [SerializeField]
    private Text thirdSkillCosts = null;

    [SerializeField]
    private Text forthSkillText = null;
    [SerializeField]
    private Text forthSkillDescription = null;

    [SerializeField]
    private Text forthSkillCosts = null;

    public void TakePlayer(PlayerController _player)
    {
        player = _player;
        player.SelectedAurea += InitView;
        player.ResetedSelection += ResetView;
        player.AbortedSkill += ResetView;
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
        firstSkillDescription.text = skills[0].GetDescription();
        firstSkillCosts.text = skills[0].GetCosts().ToString();

        secondSkillText.text = skills[1].GetName();
        secondSkillDescription.text = skills[1].GetDescription();
        secondSkillCosts.text = skills[1].GetCosts().ToString();

        thirdSkillText.text = skills[2].GetName();
        thirdSkillDescription.text = skills[2].GetDescription();
        thirdSkillCosts.text = skills[2].GetCosts().ToString();

        forthSkillText.text = skills[3].GetName();
        forthSkillDescription.text = skills[3].GetDescription();
        forthSkillCosts.text = skills[3].GetCosts().ToString();
    }

    public void UseSkill(int i)
    {
        // selectedAurea.activeSkill = selectedAurea.GetSkills()[i];
        selectedAurea.view.RPC("TakeSkill", Photon.Pun.RpcTarget.AllBuffered, i);
        IslandController.Instance.fight.JustSelectedSkill();
        ResetView();
    }
}
