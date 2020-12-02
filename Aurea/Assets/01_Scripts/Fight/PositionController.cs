using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    [SerializeField]
    private float changeSpeed = 5f;

    Transform target = null;

    void FixedUpdate()
    {
        if(target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, changeSpeed * Time.deltaTime);
        }else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, changeSpeed * Time.deltaTime);
        }
    }

    public void TakeTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
