using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlayerController : MonoBehaviour
{
    [SerializeField] private FightController controller = null;

    [SerializeField] private PlayerController player = null;

    private void Awake()
    {
        player.StartedTurn += GetDecision;
        player.GameOver += Kill;
    }

    public void GetDecision()
    {
        // Debug.Log("Want a decision");
        CreateAction();
        // StartCoroutine(WaitTillRequest());
    }

    private void FixedUpdate()
    {
        //  Debug.Log("before Decision");

        if (!player.IsOnTurn() || !controller.CanInteract()) return;

        //  Debug.Log("Ask Decision");

        this.CreateAction();
    }

    public void Kill(PlayerController player){
        StopAllCoroutines();
    }

    public  void CreateAction()
    {
        // Debug.Log("Got Decision");
        AgentInput input = new AgentInput();
        int selectedAurea = Random.Range(0, 3);
        input.selected = player.GetAureas()[selectedAurea];
        int targetAurea = Random.Range(0, 6);
        input.target = targetAurea > 2 ? GetEnemyAurea(player)[targetAurea - 3] : player.GetAureas()[targetAurea];
        int skill = Random.Range(0, 4);
        input.skill = skill;
        bool endTurn = Random.Range(0, 2) == 0;
        input.endTurn = endTurn;

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

    IEnumerator WaitTillRequest() {
        yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        CreateAction();
    }
}
