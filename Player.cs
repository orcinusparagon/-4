using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private bool Braking;
    private float currentBrakeForce;

    [Header("WheelCollider")]
    public WheelCollider FrontLeftWheelCollider;
    public WheelCollider FrontRightWheelCollider;
    public WheelCollider RearLeftWheelCollider;
    public WheelCollider RearRightWheelCollider;

    [Header("WheelTransform")]
    public Transform FrontLeftWheelTransform;
    public Transform FrontRightWheelTransform;
    public Transform RearLeftWheelTransform;
    public Transform RearRightWheelTransform;

    [Header("Car Options")]
    public float motorForce;
    public float brakeForce;
    public float maxSteerAngle;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Braking = Input.GetKey(KeyCode.Space);
    }

    private void HandleMotor()
    {
        RearLeftWheelCollider.motorTorque = verticalInput * motorForce;
        RearRightWheelCollider.motorTorque = verticalInput * motorForce;

        currentBrakeForce = Braking ? brakeForce : 0f;
        RearLeftWheelCollider.brakeTorque = currentBrakeForce;
        RearRightWheelCollider.brakeTorque = currentBrakeForce;
    }

    private void HandleSteering()
    {
        FrontLeftWheelCollider.steerAngle = horizontalInput * maxSteerAngle;
        FrontRightWheelCollider.steerAngle = horizontalInput * maxSteerAngle;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(FrontLeftWheelCollider, FrontLeftWheelTransform);
        UpdateWheelPos(FrontRightWheelCollider, FrontRightWheelTransform);
        UpdateWheelPos(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateWheelPos(RearRightWheelCollider, RearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelcollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelcollider.GetWorldPose(out pos, out rot);
        transform.position = pos;
        transform.rotation = rot;
    }
}
