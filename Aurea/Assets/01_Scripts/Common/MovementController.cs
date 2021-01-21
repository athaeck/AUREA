using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    Animator anim = null;
    Rigidbody rb = null;

    [SerializeField]
    private float speed = 12f;

    [SerializeField]
    private float arSpeedMultiplier = 0.15f;

    [Range(0, 175), SerializeField]
    private float rotationSpeed = 175;


    [SerializeField]
    private float gravity = -9.81f;

    [SerializeField]
    private float raycastDistance = 1f;

    Vector3 lastPostition = Vector3.zero;
    int counter = 3;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        lastPostition = transform.position;
    }

    private void Update()
    {
        if (counter <= 0)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 1);
        }

        counter--;

        lastPostition = transform.position;
        rb.angularVelocity = Vector3.zero;
    }

    bool isGrounded()
    {
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, raycastDistance))
        {
            if (hit.collider.CompareTag("Walkable"))
            {
                return true;
            }
        }

        return false;
    }
    public float GetSpeed() { return speed; }

    public void Move(Vector3 _direction)
    {
        Vector3 newDir = (_direction - transform.position).normalized;
        Vector3 newPos = Vector3.zero;

        if (Player.Instance.IsArOn()) 
            newPos = Vector3.Lerp(transform.position, transform.position + newDir, speed * Time.deltaTime * arSpeedMultiplier);
        else 
            newPos = Vector3.Lerp(transform.position, transform.position + newDir, speed * Time.deltaTime);

        rb.MovePosition(newPos);

        Quaternion targetRotation = Quaternion.LookRotation(_direction - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        counter = 5;
    }

    float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
