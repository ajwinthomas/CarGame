using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float maxFuel = 100f; // Maximum fuel the car can hold
    public float fuelConsumptionRate = 1f; // Fuel consumed per unit of distance
    public float fuelGainAmount = 20f; // Fuel gained by collecting a fuel can

    [Header("UI Elements")]
    public Slider fuelBar; // Reference to the UI slider
    public Image fillImage; // Reference to the fill image for changing color

    [Header("Fuel Colors")]
    public Color fullFuelColor = Color.green;
    public Color mediumFuelColor = Color.yellow;
    public Color lowFuelColor = Color.red;

    [Header("Level Manager")]
    public LevelManager levelManager; // Reference to the LevelManager script

    private float currentFuel; // Current fuel level
    private Rigidbody carRigidbody; // For calculating movement

    private void Start()
    {
        // Initialize fuel to max
        currentFuel = maxFuel;

        // Get the Rigidbody component and validate
        carRigidbody = GetComponent<Rigidbody>();
        if (carRigidbody == null)
        {
            Debug.LogError("No Rigidbody found! Please add one to the car GameObject.");
        }

        // Check UI references
        if (fuelBar == null)
        {
            Debug.LogError("Fuel bar is not assigned! Please assign it in the Inspector.");
        }

        if (fillImage == null)
        {
            Debug.LogError("Fill image is not assigned! Please assign it in the Inspector.");
        }

        if (levelManager == null)
        {
            Debug.LogError("LevelManager reference is missing! Please assign it in the Inspector.");
        }

        UpdateFuelUI(); // Initial UI update
    }

    private void Update()
    {
        // Calculate speed and consume fuel
        if (carRigidbody != null)
        {
            float speed = carRigidbody.linearVelocity.magnitude; // Magnitude of velocity
            ConsumeFuel(speed * fuelConsumptionRate * Time.deltaTime);
        }

        // Check if the fuel is empty
        if (currentFuel <= 0)
        {
            GameOver(); // Trigger game over logic
        }
    }

    private void ConsumeFuel(float amount)
    {
        // Reduce fuel, ensuring it doesn't go below 0
        currentFuel = Mathf.Clamp(currentFuel - amount, 0, maxFuel);
        UpdateFuelUI(); // Update UI to reflect changes
    }

    public void Refuel()
    {
        // Add fuel, ensuring it doesn't exceed maxFuel
        currentFuel = Mathf.Clamp(currentFuel + fuelGainAmount, 0, maxFuel);
        UpdateFuelUI(); // Update UI to reflect changes
    }

    public void ResetFuel()
    {
        currentFuel = maxFuel; // Reset fuel to max
        UpdateFuelUI(); // Update UI to reflect changes
    }

    private void UpdateFuelUI()
    {
        // Update the fuel bar slider
        if (fuelBar != null)
        {
            fuelBar.value = currentFuel / maxFuel;
        }

        // Change the fill color based on the current fuel level
        if (fillImage != null)
        {
            if (currentFuel > maxFuel * 0.5f)
            {
                fillImage.color = fullFuelColor;
            }
            else if (currentFuel > maxFuel * 0.2f)
            {
                fillImage.color = mediumFuelColor;
            }
            else
            {
                fillImage.color = lowFuelColor;
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Fuel is empty! Game Over!");

        // Call LevelManager's Game Over function if available
        if (levelManager != null)
        {
            levelManager.ShowGameOverPanel();
        }
        else
        {
            Debug.LogError("LevelManager is not assigned!");
        }

        // Pause the game
        Time.timeScale = 0f;
    }
}
