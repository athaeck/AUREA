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


    public void SetHUD(Difficulty diff, string ms)
    {
        if(difficulty != null && message != null)
        {
            difficulty.text = diff.ToString();
            message.text = ms;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
