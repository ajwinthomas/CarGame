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
        public Axel axel;
    }

    public float maxAceleration = 30.0f;
    public float brakeAceleration = 50.0f;

    public List<Wheel> Wheels;

    float moveInput;

    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        GetInputs();
    }
    void LateUpdate()
    {
        Move();
    }

    void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
    }

    void Move()
    {
        foreach (var wheel in Wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAceleration * Time.deltaTime;
        }
    }
}
