using UnityEngine;
using System;
using System.Collections.Generic;
using NUnit.Framework;
public class CarController : MonoBehaviour
{
   public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public float maxAceleration = 30.0f;
    public float brakeAceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    public List<Wheel> Wheels;

    float moveInput;
    float steerInput;

    private Rigidbody carRb;

    private CarLights carLights;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

        carLights = GetComponent<CarLights>();  
    }
    void Update()
    {
        GetInputs();
        AnimateWheels();
        WheelEffects();
    }
    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach (var wheel in Wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach(var wheel in Wheels)
        {
            if(wheel.axel == Axel.Front)
            {
                var   _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            foreach(var wheel in Wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAceleration * Time.deltaTime;
            }

            carLights.isBackLightOn = true;
            carLights.OperateBackLights();
        }
        else
        {
            foreach (var wheel in Wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }

            carLights.isBackLightOn = false;
            carLights.OperateBackLights();
        }
    }

    void AnimateWheels()
    {
        foreach(var wheel in Wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;

        }
    }

    void WheelEffects()
    {
        foreach(var wheel in Wheels)
        {
            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true  && carRb.linearVelocity.magnitude >=10.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
}
