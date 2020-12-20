using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    private Text difficulty = null;

    [SerializeField]
    private Text message = null;


    public void SetHUD(string ms)
    {
        if(difficulty != null && message != null)
        {
            message.text = ms;
        }
    }

    void Update()
    {
        difficulty.text = Player.Instance.GetDifficulty().ToString();
    }
}
