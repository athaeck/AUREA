using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // [SerializeField]
    // private Transform cam = null;

    // [SerializeField]
    // private Transform ARcam = null;

    void FixedUpdate()
    {
        transform.LookAt(transform.position + CameraController.Instance.activeCamera.transform.forward);
        // if (Player.Instance.IsArOn())
        //     transform.LookAt(transform.position + ARcam.forward);
        // else
        //     transform.LookAt(transform.position + cam.forward);
    }
}
