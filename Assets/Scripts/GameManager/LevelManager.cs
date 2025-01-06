using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject winningPanel; // Reference to the winning panel UI
    public GameObject playerCar;    // Reference to the player's car
    private AudioSource carAudioSource; // Reference to the car's audio source

    private Vector3 initialCarPosition;  // Stores the car's starting position
    private Quaternion initialCarRotation; // Stores the car's starting rotation

    private void Start()
    {
        // Save the initial position and rotation of the player's car
        initialCarPosition = playerCar.transform.position;
        initialCarRotation = playerCar.transform.rotation;

        // Ensure the winning panel is hidden at the start
        if (winningPanel != null)
        {
            winningPanel.SetActive(false);
        }

        // Get the AudioSource from the car
        carAudioSource = playerCar.GetComponent<AudioSource>();

        if (carAudioSource == null)
        {
            Debug.LogWarning("No AudioSource found on the player's car!");
        }
    }

    // Show the winning panel and pause the game
    public void ShowWinningPanel()
    {
        if (winningPanel != null)
        {
            winningPanel.SetActive(true);

            // Lower or stop the car sound
            if (carAudioSource != null)
            {
                carAudioSource.volume = 0f; // Mute the audio
                carAudioSource.Stop(); // Optionally stop the sound
            }

            Time.timeScale = 0f; // Pause the game
        }
    }

    // Restart the current level
    public void RestartLevel()
    {
        // Reset the car to its initial position and rotation
        playerCar.transform.position = initialCarPosition;
        playerCar.transform.rotation = initialCarRotation;

        // Resume car audio
        if (carAudioSource != null)
        {
            carAudioSource.volume = 1f; // Restore audio volume
            carAudioSource.Play(); // Resume playing audio
        }

        // Hide the winning panel and resume the game
        if (winningPanel != null)
        {
            winningPanel.SetActive(false);
        }

        Time.timeScale = 1f; // Resume the game
    }

    // Load the next level using the build index
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume the game
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Check if the next scene exists
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels available. Returning to main menu.");
            LoadMainMenu();
        }
    }

    // Load the main menu
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Ensure "MainMenu" exists in Build Settings
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting the game..."); // Log in the editor for debugging
        Application.Quit(); // Works only in the build
    }
}
