using TMPro;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    // Reference to the TextMeshPro object for visual feedback
    public TextMeshProUGUI indicatorText;

    // Method to show the initial "Park Here" message
    public void Start()
    {
        indicatorText.text = "Park Here"; // Initial text when the game starts
        indicatorText.color = Color.white; // Set color to white (or neutral)
    }

    // Method to show success (green color, car parked)
    public void ShowSuccess()
    {
        indicatorText.color = Color.green; // Set text color to green
        indicatorText.text = "Car Parked"; // Change text to indicate success
    }

    // Method to show failure (red color, parking incorrect)
    public void ShowFailure()
    {
        indicatorText.color = Color.red; // Set text color to red
        indicatorText.text = "Not Aligned"; // Change text to indicate failure
    }
}
