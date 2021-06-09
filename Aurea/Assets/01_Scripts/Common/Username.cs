using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Username : MonoBehaviour
{
    private PlayerData  player;
    [SerializeField]
    private GameObject inputField;
    string testing;
    // Start is called before the first frame update
    public void UsernameInput() {
        Player.Instance.setName(inputField.GetComponent<Text>().text);
        SceneManager.LoadSceneAsync("JS-Game");
    }
}
