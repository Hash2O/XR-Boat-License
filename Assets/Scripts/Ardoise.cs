using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ardoise : MonoBehaviour
{
    // Parameters for roll and pitch
    public float rollFrequency = 0.5f;
    public float rollAmplitude = 5f;
    public float pitchFrequency = 0.8f;
    public float pitchAmplitude = 5f;
    public float speed = 1f;

    // Variables to store roll and pitch state
    private Quaternion startRotation;
    private float rollTime;
    private float pitchTime;
    //[SerializeField]
    private Rigidbody shipRigidBody;

    void Start()
    {
        // Store the initial rotation of the ship
        startRotation = transform.rotation;
        shipRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update the roll and pitch time
        rollTime += Time.fixedDeltaTime * speed;
        pitchTime += Time.fixedDeltaTime * speed;

        // Calculate the roll angle using a sinusoidal function
        float roll = Mathf.Sin(rollFrequency * rollTime) * rollAmplitude;

        // Calculate the pitch angle using a sinusoidal function
        float pitch = Mathf.Sin(pitchFrequency * pitchTime) * pitchAmplitude;

        // Apply roll and pitch to the rotation of the ship
        Quaternion currentRotation = startRotation * Quaternion.Euler(-pitch, 0f, roll);
        shipRigidBody.MoveRotation(Quaternion.LookRotation(shipRigidBody.velocity, currentRotation * Vector3.up));
    }

}
