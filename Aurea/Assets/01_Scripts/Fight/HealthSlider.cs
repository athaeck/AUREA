using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public Sprite greenSprite = null;
    public Sprite orangeSprite = null;
    public Sprite redSprite = null;
    public Aurea aurea = null;
    public Slider fillSlider = null;
    public Image fillImage = null;

    private void Start()
    {
        if (!aurea) return;

        if(IslandController.Instance.activeIsland != Island.ChickenFight) {
            DestroyImmediate(gameObject);
        }

        aurea.ChangedLifepoints += UpdateLife;
    }

    void UpdateLife()
    {
        float fillAmount = aurea.GetLifePointsLeft() / aurea.GetLifePointsMax();
        fillSlider.value = fillAmount;

        if (fillAmount > 0.6f)
            fillImage.sprite = greenSprite;
        else if (fillAmount > 0.3f)
            fillImage.sprite = orangeSprite;
        else
            fillImage.sprite = redSprite;
    }
}
