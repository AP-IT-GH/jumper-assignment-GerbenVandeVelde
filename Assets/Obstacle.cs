using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float Movespeed = 3.5f;
    private Vector3 spawnpoint = Vector3.zero;
    private quaternion spawnRotation = quaternion.EulerXYZ(0, 0, 0);
    private Rigidbody rb;


    private void Start()
    {
        // Get the starting positions
        spawnpoint = transform.position;
        spawnRotation = transform.rotation;
        rb = this.GetComponent<Rigidbody>();

        // He can only move in the horizontal axis, constrain all other axis.
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        // Start moving the block
        transform.Translate(Vector3.back * Movespeed * Time.deltaTime);

        // Respawn if the block falls of the platform or goes to far
        if (transform.localPosition.y < 0 | transform.localPosition.z < -8)
        {
            transform.position = spawnpoint;
            transform.localRotation = spawnRotation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Respawn if it has collisions with the wall behind the agent or if he collides with the agent.

        if (collision.gameObject.CompareTag("Wall") | collision.gameObject.CompareTag("mlagent"))
        {
            transform.position = spawnpoint;
            transform.localRotation = spawnRotation;
            Movespeed = Random.Range(1, 10);
        }
    }
}