using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    Animator anim = null;
    Rigidbody rb = null;
    public Vector3 destination { get; set; }

    [SerializeField]
    private float speed = 12f;

    [Range(0, 175), SerializeField]
    private float rotationSpeed = 175;

    [SerializeField]
    private float threshold = 1f;

    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float raycastDistance = 1f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        destination = transform.position;
    }

    private void FixedUpdate()
    {
        if ((destination - transform.position).magnitude > threshold)
        {
            Vector3 dir = (destination - transform.position).normalized * speed * Time.deltaTime;

            if (!isGrounded())
                rb.AddForce(transform.up * gravity, ForceMode.Acceleration);

            rb.MovePosition(transform.position + dir);

            Vector3 newRot = Vector3.Slerp(transform.position + transform.forward, destination, rotationSpeed * Time.deltaTime);
            transform.LookAt(newRot);

            float animSpeed = Remap(dir.magnitude, 0, 0.1f, 0, 1);
            anim.SetFloat("Speed", animSpeed);
        } else {
            destination = transform.position;
            anim.SetFloat("Speed", 0);
        }

        rb.angularVelocity = Vector3.zero;
    }

    bool isGrounded()
    {
        if(Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance)) {
            if(hit.collider.CompareTag("Walkable")) {
                return true;
            }
        }

        return false;
    }
    public float GetSpeed() { return speed; }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
