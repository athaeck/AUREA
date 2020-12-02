using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField]
    private Transform cam = null;

    void FixedUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
