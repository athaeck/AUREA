using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Baumkrone : MonoBehaviour
{
    Aurea aurea = null;
    List<Aurea> enemyAurea = new List<Aurea>();

    [SerializeField]
    int roundsLeft = 1;

    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.GetPlayer().StartedTurn += NewTurn;
        enemyAurea = GetEnemyAurea();
        foreach (Aurea enemy in enemyAurea)
        {
            enemy.StartAttack += BlockAttack;
        }
    }

    void BlockAttack(Damage _dmg)
    {
        _dmg.targets = new List<Aurea>();
        _dmg.targets.Add(aurea);
    }

    void NewTurn()
    {
        roundsLeft--;
        if (roundsLeft <= 0)
            Kill();
    }

    public void Kill()
    {
        foreach (Aurea enemy in enemyAurea)
        {
            enemy.StartAttack -= BlockAttack;
        }
        if (aurea != null && aurea.GetPlayer() != null)
            aurea.GetPlayer().StartedTurn -= NewTurn;
        DestroyImmediate(this);
    }

    private List<Aurea> GetEnemyAurea()
    {
        PlayerController controllerPlayer = IslandController.Instance.fight.GetPlayer();
        PlayerController controllerEnemy = IslandController.Instance.fight.GetEnemy();

        foreach (Aurea _aurea in controllerPlayer.GetAureas())
        {
            if (_aurea == aurea)
                return controllerEnemy.GetAureas();
        }

        foreach (Aurea _aurea in controllerEnemy.GetAureas())
        {
            if (_aurea == aurea)
                return controllerPlayer.GetAureas();
        }

        return new List<Aurea>();
    }
}
