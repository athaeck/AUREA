using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TimeVisualizationController : MonoBehaviour
{
    public Sprite greenSprite = null;
    public Sprite orangeSprite = null;
    public Sprite redSprite = null;
    public FightController fightController = null;
    public Slider timeSlider = null;
    public Image fillImage = null;

    public bool timerStarted = false;
    public float roundTime = 30f;
    public float timeLeft = 0f;

    public Photon.Pun.PhotonView view = null;

    public void StartTimer() {
        timeLeft = roundTime;
        timerStarted = true;
    }

    public void EndTimer() {
        timerStarted = false;
    }

    private void UpdateUI() {
        float fillAmount = timeLeft / roundTime;
        timeSlider.value = fillAmount;
        
        if(fillAmount > 0.6f)
            fillImage.sprite = greenSprite;
        else if(fillAmount > 0.3f)
            fillImage.sprite = orangeSprite;
        else
            fillImage.sprite = redSprite;
    }

    [PunRPC]
    public void SetTimeLeft(float _time) {
        timeLeft = _time;
        UpdateUI();
    }


    private void Update()
    {

        // SetTimeLeft(timeLeft - Time.deltaTime);
        if(PhotonNetwork.IsMasterClient) {
            view.RPC("SetTimeLeft", RpcTarget.All, timeLeft - Time.deltaTime);
        }
    }
}
