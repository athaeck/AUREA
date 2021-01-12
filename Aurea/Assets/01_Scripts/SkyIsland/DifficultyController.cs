using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    [SerializeField]
    private GameObject easyLevel = null;

    [SerializeField]
    private GameObject normalLevel = null;

    [SerializeField]
    private GameObject hardLevel = null;

    private Difficulty currentDifficulty;

    void Awake()
    {
        currentDifficulty = Player.Instance.GetDifficulty();
    }

    // Update is called once per frame
    void Update()
    {
        currentDifficulty = Player.Instance.GetDifficulty();
        if(easyLevel != null && normalLevel != null && hardLevel != null)
        {
            ActivateDifficulty();
        }
    }
    private void ActivateDifficulty()
    {
        switch(currentDifficulty)
        {
            case Difficulty.Medium:
                easyLevel.SetActive(false);
                normalLevel.SetActive(true);
                hardLevel.SetActive(false);
                break;
            case Difficulty.Difficult:
                easyLevel.SetActive(false);
                normalLevel.SetActive(false);
                hardLevel.SetActive(true);
                break;
            case Difficulty.Easy:
                easyLevel.SetActive(true);
                normalLevel.SetActive(false);
                hardLevel.SetActive(false);
                break;

        }
    }
    public void SetDifficulty()
    {
        switch(currentDifficulty)
        {
            case Difficulty.Medium:
                Player.Instance.SetDifficulty(Difficulty.Difficult);
                break;
            case Difficulty.Difficult:
                Player.Instance.SetDifficulty(Difficulty.Easy);
                break;
            case Difficulty.Easy:
                Player.Instance.SetDifficulty(Difficulty.Medium);
                break;
        }
    }
}
