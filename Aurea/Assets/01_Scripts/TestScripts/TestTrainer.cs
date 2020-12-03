using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(Rigidbody))]
public class TestTrainer : Agent
{
    public float speed = 3f;
    public float maxX = 5f;
    public float maxY = 5f;
    public Transform target = null;
    Rigidbody rb = null;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = RndPos();
        target.localPosition = RndPos();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-(1f / MaxStep));
        if (transform.localPosition.y < -2f)
        {
            EndEpisode();
        }

        Vector3 movement = Vector3.zero;
        movement.x += vectorAction[0] * Time.deltaTime * speed;
        movement.y = rb.velocity.y;
        movement.z += vectorAction[1] * Time.deltaTime * speed;
        rb.velocity = movement;
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
            EndEpisode();
    }

    public Vector3 RndPos()
    {
        float x = Random.Range(-maxX, maxX);
        float z = Random.Range(-maxY, maxY);
        return new Vector3(x, 0.5f, z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Target"))
        {
            AddReward(1f);
            EndEpisode();
        }
    }
}
