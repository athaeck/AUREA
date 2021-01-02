using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PositionController))]
public class TurnCrystalController : MonoBehaviour
{
    [SerializeField]
    private FightController controller = null;

    [SerializeField]
    private PositionController positionController = null;

    [SerializeField]
    private Transform playerPosition = null;

    [SerializeField]
    private Transform enemyPosition = null;

    private void Start()
    {
        positionController = GetComponent<PositionController>();
        controller.TurnChanged += TurnChanged;
    }

    private void TurnChanged(PlayerController _playerController)
    {
        if (_playerController.isPlayer)
            positionController.TakeTarget(playerPosition);
        else
            positionController.TakeTarget(enemyPosition);
    }
}
