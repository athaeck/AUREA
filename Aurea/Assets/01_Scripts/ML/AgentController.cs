using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentInput
{
    public Aurea selected;
    public Aurea target;
    public int skill;
    public bool endTurn;
}

public class AgentController : Agent
{
    [SerializeField] private FightController controller = null;

    [SerializeField] private PlayerController player = null;

    private void Awake()
    {
        player.StartedTurn += GetDecision;
    }

    public void GetDecision()
    {
        Debug.Log("Want a decision");
        this.RequestDecision();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(player.GetAPLeft());
        foreach (Aurea aurea in player.GetAureas())
        {
            sensor.AddObservation(int.Parse(aurea.GetAureaData().ID));
            sensor.AddObservation(aurea.GetLifePointsLeft());
        }

        PlayerController enemy = GetEnemy(player);
        sensor.AddObservation(enemy.GetAPLeft());
        foreach (Aurea aurea in enemy.GetAureas())
        {
            sensor.AddObservation(int.Parse(aurea.GetAureaData().ID));
            sensor.AddObservation(aurea.GetLifePointsLeft());
        }
    }

    public override void OnActionReceived(ActionBuffers _actionBuffers)
    {
        AgentInput input = new AgentInput();
        input.selected = player.GetAureas()[_actionBuffers.DiscreteActions[0]];
        input.target = _actionBuffers.DiscreteActions[1] > 2 ? GetEnemyAurea(player)[_actionBuffers.DiscreteActions[1] - 3] : player.GetAureas()[_actionBuffers.DiscreteActions[1]];
        input.skill = _actionBuffers.DiscreteActions[2];
        input.endTurn = _actionBuffers.DiscreteActions[3] == 0;

        controller.TakeAgentInput(input);
    }

    private List<Aurea> GetEnemyAurea(PlayerController _player)
    {
        PlayerController controllerPlayer = IslandController.Instance.fight.GetPlayer();
        PlayerController controllerEnemy = IslandController.Instance.fight.GetEnemy();

        if (_player == controllerPlayer)
            return controllerEnemy.GetAureas();
        else
            return controllerPlayer.GetAureas();
    }

    private PlayerController GetEnemy(PlayerController _player)
    {
        PlayerController controllerPlayer = IslandController.Instance.fight.GetPlayer();
        PlayerController controllerEnemy = IslandController.Instance.fight.GetEnemy();

        if (_player == controllerPlayer)
            return controllerEnemy;
        else
            return controllerPlayer;
    }

}


