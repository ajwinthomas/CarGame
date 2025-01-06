using UnityEngine;

public class FuelCan : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Transform rootTransform = other.transform.root;

        // Check if the object colliding has the "Player" tag
        if (other.transform.root.CompareTag("Player"))
        {
            Debug.Log("hiilj");
            // Get the Rigidbody or FuelSystem from the root object (the parent)
            
            FuelSystem fuelSystem = rootTransform.GetComponent<FuelSystem>();

            if (fuelSystem != null)
            {
                fuelSystem.Refuel(); // Refill the car's fuel
                Destroy(gameObject); // Destroy the fuel can
            }
            else
            {
                Debug.LogWarning("FuelSystem component not found on the player root object!");
            }
        }
    }
}
