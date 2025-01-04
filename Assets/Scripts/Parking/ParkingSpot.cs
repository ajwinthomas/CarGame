using System;
using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    // Reference to the IndicatorController for feedback
    public IndicatorController indicatorController;

    // Position tolerance for parking (distance from the center of the spot)
    public float positionTolerance = 1.0f;

    // Rotation tolerance for parking (angle in degrees)
    public float rotationTolerance = 10.0f;

    // Reference to the player's car inside the parking spot
    private GameObject playerCar;

    // Update is called once per frame
    private void Update()
    {
        // Check parking status continuously if the player's car is in the parking spot
        if (playerCar != null)
        {
            if (IsCarParkedCorrectly(playerCar))
            {
                indicatorController.ShowSuccess(); // Show success if parked correctly
            }
            else
            {
                indicatorController.ShowFailure(); // Show failure if not parked correctly
            }
        }
    }

    // OnTriggerEnter to detect when the car enters the parking spot
    private void OnTriggerEnter(Collider other)
    {
        // Get the root GameObject in case the collider is on a child object
        GameObject rootObject = other.transform.root.gameObject;

        // Check if the root object is tagged as "Player"
        if (rootObject.CompareTag("Player"))
        {
            // Store a reference to the player's car
            playerCar = rootObject;
        }
    }

    // OnTriggerExit to clear the car reference when it leaves the parking spot
    private void OnTriggerExit(Collider other)
    {
        // Get the root GameObject in case the collider is on a child object
        GameObject rootObject = other.transform.root.gameObject;

        // Check if the root object is tagged as "Player"
        if (rootObject.CompareTag("Player"))
        {
            // Clear the reference to the player's car
            playerCar = null;

        }
    }

    // Method to validate if the car is parked correctly
    private bool IsCarParkedCorrectly(GameObject car)
    {
        // Check position: Is the car within the position tolerance?
        float distanceToCenter = Vector3.Distance(car.transform.position, transform.position);
        if (distanceToCenter > positionTolerance)
        {
            return false;
        }

        // Check rotation: Is the car aligned with the parking spot?
        float angleDifference = Quaternion.Angle(car.transform.rotation, transform.rotation);
        if (angleDifference > rotationTolerance)
        {
            return false;
        }

        // If both checks pass, the car is parked correctly
        return true;
    }
}
