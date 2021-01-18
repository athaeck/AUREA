using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeVisualizationController : MonoBehaviour
{
    public Sprite greenSprite = null;
    public Sprite orangeSprite = null;
    public Sprite redSprite = null;
    public FightController fightController = null;
    public Slider timeSlider = null;
    public Image fillImage = null;
    private void Update()
    {
        if(!fightController) return;

        float fillAmount = fightController.timeLeft / fightController.roundTime;
        timeSlider.value = fillAmount;
        
        if(fillAmount > 0.6f)
            fillImage.sprite = greenSprite;
        else if(fillAmount > 0.3f)
            fillImage.sprite = orangeSprite;
        else
            fillImage.sprite = redSprite;
    }
}
