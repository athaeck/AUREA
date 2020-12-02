using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement controller for the characters
/// </summary>
public class MovementController : MonoBehaviour
{
    /// <summary>
    /// Set the movement speed of the character
    /// </summary>
    [SerializeField]
    private float speed = 12f;

    /// <summary>
    /// Set the rotation speed of the character
    /// </summary>
    [Range(0, 175), SerializeField]
    private float rotaionSpeed = 175;

    /// <summary>
    /// Move function. This function just moves the character forward
    /// </summary>
    /// <param name="_value">The amount of movement. Keep this value between 0 and 1.</param>
    public void Move(float _value)
    {
        // Linear interpolate to the new position. It moves the actuel position towards a forward vector multiplied with the input value. The hole movement is based on the speed value
        Vector3 newPos = Vector3.Lerp(transform.position, transform.position + (transform.forward * _value), speed * Time.fixedDeltaTime);

        // Set the calculated position as the new position of the transform
        transform.position = newPos;
    }

    /// <summary>
    /// Rotate function. Use this funtion to rotate the character
    /// </summary>
    /// <param name="_value">The amount of rotation. Keep this value between 0 and 1.</param>
    public void Rotate(float _value)
    {
        // First create a new Vector3 which representate the actual rotation and add the amount of rotation multiplied by the speed
        Vector3 rot = transform.localRotation.eulerAngles;
        rot.y += _value * rotaionSpeed;

        // Then create a Quaternion to save the euler representation in a Quaternion Object
        Quaternion resRot = Quaternion.identity;
        resRot.eulerAngles = rot;

        // Then Spherical interpolate between the actual rotation and the desired rotation
        Quaternion resultRot = Quaternion.Slerp(transform.localRotation, resRot, Time.fixedDeltaTime);

        //Finaly set the calculated value as the new rotation
        transform.localRotation = resultRot;
    }
}
