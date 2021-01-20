using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    [SerializeField]
    bool freezeX = false;

    [SerializeField]
    bool freezeY = false;

    [SerializeField]
    bool freezeZ = false;

    private void LateUpdate()
    {
        Vector3 newRot = Vector3.zero;
        newRot.x = freezeX ? 0 : transform.localEulerAngles.x;
        newRot.y = freezeY ? 0 : transform.localEulerAngles.y;
        newRot.z = freezeZ ? 0 : transform.localEulerAngles.z;

        transform.localEulerAngles = newRot;
    }
}
