using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Username : MonoBehaviour
{

    [SerializeField]
    private GameObject inputField;

    [SerializeField]
    private GameObject UserNameHUD = null;
    // Start is called before the first frame update
    public void UsernameInput() {
        Player.Instance.setName(inputField.GetComponent<Text>().text);
        StateManager.SavePlayer(Player.Instance);
        UserNameHUD.SetActive(false);
    }
}
