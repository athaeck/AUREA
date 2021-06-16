using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Kristallisiert : MonoBehaviour
{
    [SerializeField]
    float attackMultiplier = 0.6f;

    [SerializeField]
    int neededSplitter = 5;

    [SerializeField]
    float waitTillAttack = 0.5f;

    Aurea aurea = null;
    int splitter = 0;
    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.StartAttack += AddShardSplitter;
    }

    void AddShardSplitter(Damage _dmg)
    {
        AddSplitter(1);
    }

    void StartSplitterAttack()
    {
        List<Aurea> aureaList = GetEnemyAurea(aurea.view.ViewID);
        if (aureaList.Count <= 0)
        {
            // Debug.Log("Didnt found enemy Aurea");
            return;
        }

        for (int i = 1; i <= neededSplitter; i++)
        {
            int rnd = UnityEngine.Random.Range(0, aureaList.Count);
            Aurea attackAurea = aureaList[rnd];

            Damage dmg = aurea.GetDamage();
            dmg.physicalDamage *= attackMultiplier;
            dmg.targets.Add(attackAurea);

            if (Player.Instance.animationsOn)
                StartCoroutine(Attack(waitTillAttack * i, dmg));
            else
                attackAurea.TakeDamage(dmg);

            splitter--;
        }



        if (splitter >= neededSplitter)
        {
            if (Player.Instance.animationsOn)
                StartCoroutine(WaitTillAttackAgain(neededSplitter * waitTillAttack));
            else
                StartSplitterAttack();
        }
    }
    protected List<Aurea> GetEnemyAurea(int _viewID) {
        List<PlayerController> players = IslandController.Instance.fight.players;
        foreach (Aurea _aurea in players[0].GetAureas())
        {
            if (_aurea.view.ViewID == _viewID)
                return players[1].GetAureas();
        }
        foreach (Aurea _aurea in players[1].GetAureas())
        {
            if (_aurea.view.ViewID == _viewID)
                return players[0].GetAureas();
        }

        Debug.Log("No Aurea found");
        return new List<Aurea>();
    }

    // List<Aurea> GetEnemyAurea()
    // {
    //     PlayerController controllerPlayer = IslandController.Instance.fight.GetPlayer();
    //     PlayerController controllerEnemy = IslandController.Instance.fight.GetEnemy();

    //     foreach (Aurea _aurea in controllerPlayer.GetAureas())
    //     {
    //         if (_aurea == aurea)
    //             return controllerEnemy.GetAureas();
    //     }

    //     foreach (Aurea _aurea in controllerEnemy.GetAureas())
    //     {
    //         if (_aurea == aurea)
    //             return controllerPlayer.GetAureas();
    //     }

    //     return new List<Aurea>();
    // }

    public void AddSplitter(int _amount)
    {
        splitter += _amount;

        if (splitter >= neededSplitter)
            StartSplitterAttack();
    }

    IEnumerator Attack(float _time, Damage _dmg)
    {
        yield return new WaitForSeconds(_time);
        _dmg.targets[0].TakeDamage(_dmg);
    }

    IEnumerator WaitTillAttackAgain(float _time)
    {
        yield return new WaitForSeconds(_time);
        StartSplitterAttack();
    }
}
