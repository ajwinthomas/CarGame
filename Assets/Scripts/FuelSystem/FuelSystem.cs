using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    [Header("Fuel Settings")]
    public float maxFuel = 100f;
    public float currentFuel;
    public float depletionRate = 1f;

    [Header("UI References")]
    public Slider fuelBar; // Reference to the fuel bar UI slider
    private Image fillImage; // Reference to the fill area of the fuel bar

    private Rigidbody carRigidbody;

    // Events for external interaction (like Game Over or Low Fuel)
    public delegate void FuelEmpty();
    public static event FuelEmpty OnFuelEmpty;

    void Start()
    {
        InitializeFuelSystem();
    }

    void Update()
    {
        if (currentFuel > 0)
        {
            DepleteFuel();
            UpdateFuelUI();
        }
        else
        {
            TriggerFuelEmpty();
        }
    }

    #region Fuel Management

    private void InitializeFuelSystem()
    {
        currentFuel = maxFuel;
        carRigidbody = GetComponent<Rigidbody>();
        fillImage = fuelBar.fillRect.GetComponent<Image>(); // Get the fill image component
        UpdateFuelUI();
    }

    private void DepleteFuel()
    {
        float speed = carRigidbody.linearVelocity.magnitude;
        currentFuel -= depletionRate * speed * Time.deltaTime;
        currentFuel = Mathf.Clamp(currentFuel, 0, maxFuel);
    }

    private void UpdateFuelUI()
    {
        fuelBar.value = currentFuel / maxFuel;
        UpdateFuelColor();
    }

    private void UpdateFuelColor()
    {
        if (currentFuel > 50)
        {
            fillImage.color = Color.green;
        }
        else if (currentFuel > 20)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }

    #endregion

    #region Game Over & Event Handling

    private void TriggerFuelEmpty()
    {
        OnFuelEmpty?.Invoke();  // Trigger event if fuel is empty

        // Additional game over logic can go here (e.g., disabling car controls)
        Debug.Log("Game Over: Out of Fuel");
    }

    #endregion
}
