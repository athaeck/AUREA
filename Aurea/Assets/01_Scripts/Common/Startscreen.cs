using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startscreen : MonoBehaviour
{
    [SerializeField]
    private GameObject buttons = null;

    [SerializeField]
    private GameObject options = null;

    public void StartButton()
    {
        gameObject.SetActive(false);
        if (Player.Instance.NAME == "")
        {
            SceneManager.LoadSceneAsync("Username");
        }   
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
