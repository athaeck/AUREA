using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    private FightController controller = null;

    [SerializeField]
    private GameObject selectedHUD = null;

    [SerializeField]
    private GameObject targetHUD = null;

    private SelectedHUDController selectedHUDController = null;
    private TargetHUDController targetHUDController = null;

    PlayerController player = null;

    private void Awake()
    {
        selectedHUDController = selectedHUD.GetComponent<SelectedHUDController>();
        targetHUDController = targetHUD.GetComponent<TargetHUDController>();
        controller.LoadedPlayer += HandleLoadPlayer;
    }

    private void HandleLoadPlayer(PlayerController playerLoaded)
    {
        player = playerLoaded;
        player.SelectedAurea += HandleAureaSelect;
        player.SelectedTarget += HandleTargetSelect;
        player.ResetedSelection += HandleSelectedReset;
    }

    private void HandleSelectedReset()
    {
        selectedHUD.SetActive(false);
        targetHUD.SetActive(false);
    }

    private void HandleAureaSelect(Aurea aurea)
    {
        selectedHUD.SetActive(true);
        selectedHUDController.TakeTarget(aurea);    }

    private void HandleTargetSelect(Aurea aurea)
    {
        targetHUD.SetActive(true);
        targetHUDController.TakeTarget(aurea);
    }
}
