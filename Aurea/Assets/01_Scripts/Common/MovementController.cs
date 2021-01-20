using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    CharacterController controller;
    public Vector3 destination { get; set; }

    [SerializeField]
    private float speed = 12f;

    [Range(0, 175), SerializeField]
    private float rotationSpeed = 175;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 dir = (destination - transform.position).normalized * speed * Time.deltaTime;
        controller.Move(dir);

        Vector3 newRot = Vector3.Slerp(transform.position + transform.forward, destination, rotationSpeed * Time.deltaTime);
        transform.LookAt(newRot);

    }
    public float GetSpeed() { return speed; }

}
