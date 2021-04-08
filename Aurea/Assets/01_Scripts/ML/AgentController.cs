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
        // Academy.Instance.AutomaticSteppingEnabled = false;
        player.StartedTurn += GetDecision;
    }

    public void GetDecision()
    {
        // Debug.Log("Want a decision");
        // StartCoroutine(WaitTillRequest());
        this.RequestDecision();
    }

    // private void FixedUpdate()
    // {
    //     //  Debug.Log("before Decision");

    //     if (!player.IsOnTurn() || !controller.CanInteract()) return;

    //     //  Debug.Log("Ask Decision");

    //     this.RequestDecision();
    // }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Mathf.Clamp(player.GetAPLeft(), 0, 15));
        foreach (Aurea aurea in player.GetAureas())
        {
            sensor.AddObservation(Mathf.Clamp(int.Parse(aurea.GetAureaData().ID), 0, 15));
            sensor.AddObservation(Mathf.Clamp(aurea.GetLifePointsLeft(), 0 , float.MaxValue));
        }

        PlayerController enemy = GetEnemy(player);
        sensor.AddObservation(Mathf.Clamp(enemy.GetAPLeft(), 0, 15));
        foreach (Aurea aurea in enemy.GetAureas())
        {
            sensor.AddObservation(Mathf.Clamp(int.Parse(aurea.GetAureaData().ID), 0, 15));
            sensor.AddObservation(Mathf.Clamp(aurea.GetLifePointsLeft(), 0 , float.MaxValue));
        }
    }

    public override void OnEpisodeBegin()
    {
        
        // Debug.LogError("End Episode");
        StopAllCoroutines();
        // player.GameOver?.Invoke(player);
    }

    public override void OnActionReceived(ActionBuffers _actionBuffers)
    {
        // Debug.Log("Got Decision");

        if (!player.IsOnTurn() || !controller.CanInteract())
            return;

        // Debug.Log("Do turn");
        
        // Academy.Instance.EnvironmentStep();

        AgentInput input = new AgentInput();
        input.selected = player.GetAureas()[_actionBuffers.DiscreteActions[0]];
        input.target = _actionBuffers.DiscreteActions[1] > 2 ? GetEnemyAurea(player)[_actionBuffers.DiscreteActions[1] - 3] : player.GetAureas()[_actionBuffers.DiscreteActions[1]];
        input.skill = _actionBuffers.DiscreteActions[2];
        input.endTurn = _actionBuffers.DiscreteActions[3] == 0;

        // Debug.Log(_actionBuffers.DiscreteActions[0]);
        // Debug.Log(_actionBuffers.DiscreteActions[1]);
        // Debug.Log(_actionBuffers.DiscreteActions[2]);
        // Debug.Log(_actionBuffers.DiscreteActions[3]);

        // Debug.Log(input.endTurn);

        // controller.TakeAgentInput(input);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
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

    IEnumerator WaitTillRequest() {
        yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        this.RequestDecision();
    }

}


