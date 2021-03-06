using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aurea))]
public class Atemraub : MonoBehaviour
{
    private float healthDampAmount = 0.3f;
    private Aurea aurea = null;

    private int amountOfRounds = 5;

    private int roundsLeft = 0;
    void Start()
    {
        aurea = GetComponent<Aurea>();
        aurea.BeforeChangeLifepoints += CheckIfIsHeal;
        aurea.GetPlayer().StartedTurn += NewTurn;
        roundsLeft = amountOfRounds;
    }

    void NewTurn()
    {
        roundsLeft--;
        if (roundsLeft <= 0)
            Kill();
    }

    void CheckIfIsHeal(float _amount) {

        if(_amount > 0) return;

        float dampAmount = -_amount * healthDampAmount;
        float lifePointsLeft = aurea.GetLifePointsLeft();
        aurea.SetLifePointsLeft(lifePointsLeft - dampAmount);
    }

    public void Kill()
    {
        aurea.BeforeChangeLifepoints -= CheckIfIsHeal;
        DestroyImmediate(this);
    }
    
}
