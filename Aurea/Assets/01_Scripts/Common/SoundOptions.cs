using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField]
    private AudioController audioController = null;

    [SerializeField]
    private Slider backgroundVolume = null;

    [SerializeField]
    private Slider sfxVolume = null;
    
    public void Vol()
    {
        float bgVol = backgroundVolume.value;
        float sfxVol = sfxVolume.value;
        audioController.ChangeVolume(bgVol, sfxVol);
        Player.Instance.setVol(bgVol, sfxVol);
    }

    public void setSlider()
    {
        backgroundVolume.value = Player.Instance.backgroundVol;
        sfxVolume.value = Player.Instance.sfxVol;
    }
}
