using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationController : MonoBehaviour
{
    [SerializeField]
    private FightController controller = null;

    [SerializeField]
    private PositionController selectedParticles = null;

    [SerializeField]
    private PositionController targetParticles = null;

    [SerializeField]
    private PositionController turnObject = null;

    [SerializeField]
    private Transform playerTurnPosition = null;

    [SerializeField]
    private Transform enemyTurnPosition = null;

    [SerializeField]
    private CrystalVisualizationController playerCrystals = null;

    [SerializeField]
    private CrystalVisualizationController enemyCrystals = null;

    [SerializeField]
    private SelectedHUDController selectedHUDController = null;

    [SerializeField]
    private TargetHUDController targetHUDController = null;

    PlayerController player = null;

    void Awake()
    {
        controller.TurnChanged += ChangeTurn;
        controller.LoadedPlayer += HandlePlayerLoad;
        controller.LoadedEnemy += HandleEnemyLoad;
        controller.StartedUsingSkill += HandleStartUsingSkill;
        controller.EndedUsingSkill += HandleEndUsingSkill;
        controller.GameEnded += ResettedSelections;
    }

    void HandlePlayerLoad(PlayerController player)
    {
        this.player = player;
        player.ChangedAP += UpdatePlayerCrystals;
        player.SelectedAurea += SelectedAurea;
        player.SelectedTarget += TargetedAurea;
        player.ResetedSelection += ResettedSelections;
        player.ResetTarget += ResetTarget;
    }

    void HandleEnemyLoad(PlayerController enemy)
    {
        enemy.ChangedAP += UpdateEnemyCrystals;
        enemy.SelectedAurea += SelectedAureaEnemy;
        enemy.SelectedTarget += TargetedAureaEnemy;
        enemy.ResetedSelection += ResettedSelectionsEnemy;
    }

    void HandleStartUsingSkill()
    {
        selectedHUDController.gameObject.SetActive(false);
    }

    void HandleEndUsingSkill()
    {
        if(player.IsOnTurn())
            selectedHUDController.gameObject.SetActive(true);
    }

    void UpdatePlayerCrystals(int amount)
    {
        playerCrystals.ActiveCrystals(amount);
    }

    void UpdateEnemyCrystals(int amount)
    {
        enemyCrystals.ActiveCrystals(amount);
    }

    void ChangeTurn(PlayerController player)
    {
        if (player.isPlayer)
            turnObject.TakeTarget(playerTurnPosition);
        else
            turnObject.TakeTarget(enemyTurnPosition);
    }

    void ResettedSelections()
    {
        ResetSelected();
        ResetTarget();
    }

    void ResetSelected()
    {
        selectedParticles.TakeTarget(null);
        selectedHUDController.gameObject.SetActive(false);
    }

    void ResetTarget()
    {
        targetParticles.TakeTarget(null);
        targetHUDController.gameObject.SetActive(false);
    }
    void SelectedAurea(Aurea selected)
    {
        selectedParticles.TakeTarget(selected.transform);
        selectedHUDController.TakeTarget(selected);
        selectedHUDController.gameObject.SetActive(true);
    }

    void TargetedAurea(Aurea target)
    {
        targetParticles.TakeTarget(target.transform);
        targetHUDController.TakeTarget(target);
        targetHUDController.gameObject.SetActive(true);
    }

    void ResettedSelectionsEnemy()
    {
        selectedParticles.TakeTarget(null);
        targetParticles.TakeTarget(null);
    }

    void SelectedAureaEnemy(Aurea selected)
    {
        selectedParticles.TakeTarget(selected.transform);
        selectedHUDController.TakeTarget(selected);
    }

    void TargetedAureaEnemy(Aurea target)
    {
        targetParticles.TakeTarget(target.transform);
        targetHUDController.TakeTarget(target);
    }
}
