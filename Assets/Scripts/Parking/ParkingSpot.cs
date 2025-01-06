using UnityEngine;

public class ParkingSpot : MonoBehaviour
{
    public IndicatorController indicatorController;
    public float positionTolerance = 1.0f;
    public float rotationTolerance = 10.0f;
    public LevelManager levelManager; // Reference LevelManager here.

    private GameObject playerCar;
    private bool isCarParkedCorrectly = false;

    private void Update()
    {
        if (playerCar != null)
        {
            if (IsCarParkedCorrectly(playerCar))
            {
                if (!isCarParkedCorrectly)
                {
                    isCarParkedCorrectly = true;
                    indicatorController.ShowSuccess();

                    // Notify LevelManager when parking is successful
                    if (levelManager != null)
                    {
                        levelManager.ShowWinningPanel();
                    }
                }
            }
            else
            {
                isCarParkedCorrectly = false;
                indicatorController.ShowFailure();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject rootObject = other.transform.root.gameObject;
        if (rootObject.CompareTag("Player"))
        {
            playerCar = rootObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GameObject rootObject = other.transform.root.gameObject;
        if (rootObject.CompareTag("Player"))
        {
            playerCar = null;
            isCarParkedCorrectly = false;
            indicatorController.ShowFailure();
        }
    }

    private bool IsCarParkedCorrectly(GameObject car)
    {
        float distanceToCenter = Vector3.Distance(car.transform.position, transform.position);
        if (distanceToCenter > positionTolerance)
        {
            return false;
        }

        float angleDifference = Quaternion.Angle(car.transform.rotation, transform.rotation);
        if (angleDifference > rotationTolerance)
        {
            return false;
        }

        return true;
    }
}
