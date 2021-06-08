using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleController : MonoBehaviour
{
    private Slider sizeSlider = null;
    private float initARScaling = 0.01f;

    [SerializeField]
    private ARToolkitController aRToolkitController = null;

    private List<ParticleSystem> pS = new List<ParticleSystem>();
    void Start()
    {
        // pS = this.GetComponent<ParticleSystem>();
        // Debug.Log(pS);
        pS.Add(this.GetComponent<ParticleSystem>());
        ParticleSystem[] list = this.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem P in list)
        {
            pS.Add(P);
        }
        if (aRToolkitController != null)
        {
            sizeSlider = aRToolkitController.GetSizeSlider();
            initARScaling = aRToolkitController.GetInitARScaling();
        }
        Scale();
    }

    void Update()
    {
        Scale();
    }
    private void Scale()
    {
        if (Player.Instance.IsArOn())
        {
            if (pS != null)
            {
                Vector3 newSize = new Vector3(sizeSlider.value, sizeSlider.value, sizeSlider.value) * initARScaling;
                Debug.Log(newSize);
                foreach (ParticleSystem p in pS)
                {
                    p.transform.localScale = newSize;
                    // Slider scale = new Slider().;
                    // p.shape.scale = newSize;
                    ParticleSystem.ShapeModule shape = p.shape;
                    shape.scale = newSize;
                    // p.
                    // p.main.startSize.constantMin = p.main.startSize.constantMin * initARScaling;
                    //  = newSize;
                }

            }
        }
    }
}
