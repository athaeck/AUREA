using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startscreen : MonoBehaviour
{
    [SerializeField]
    private GameObject buttons = null;

    [SerializeField]
    private GameObject options = null;

    public void StartButton()
    {
        gameObject.SetActive(false);
    }

    public void Options()
    {
        //options.GetComponent<SoundOptions>().setSlider();
        options.SetActive(true);
    }

    public void OptionsExit()
    {
        options.SetActive(false);
        StateManager.SavePlayer(Player.Instance);
    }

    public void ResetPlayer()
    {
        StateManager.CreateNewPlayerData();
    }
}
