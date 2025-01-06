using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject winningPanel; // Reference to the winning panel UI
    public GameObject gameOverPanel; // Reference to the game over panel UI

    [Header("Player Settings")]
    public GameObject playerCar; // Reference to the player's car

    private AudioSource carAudioSource; // Reference to the car's audio source
    private Vector3 initialCarPosition; // Stores the car's starting position
    private Quaternion initialCarRotation; // Stores the car's starting rotation

    private FuelSystem fuelSystem; // Reference to the FuelSystem script

    private void Awake()
    {
        // Validate references in case they are not assigned in the Inspector
        if (playerCar == null)
        {
            Debug.LogError("Player car is not assigned. Please assign it in the Inspector.");
        }

        if (winningPanel == null)
        {
            Debug.LogWarning("Winning panel is not assigned. Please assign it in the Inspector.");
        }

        if (gameOverPanel == null)
        {
            Debug.LogWarning("Game over panel is not assigned. Please assign it in the Inspector.");
        }

        // Get the FuelSystem component from the player's car
        if (playerCar != null)
        {
            fuelSystem = playerCar.GetComponent<FuelSystem>();
            if (fuelSystem == null)
            {
                Debug.LogWarning("No FuelSystem found on the player's car!");
            }
        }
    }

    private void Start()
    {
        // Save the initial position and rotation of the player's car
        if (playerCar != null)
        {
            initialCarPosition = playerCar.transform.position;
            initialCarRotation = playerCar.transform.rotation;

            // Get the AudioSource from the car
            carAudioSource = playerCar.GetComponent<AudioSource>();
            if (carAudioSource == null)
            {
                Debug.LogWarning("No AudioSource found on the player's car!");
            }
        }

        // Ensure the winning panel and game over panel are hidden at the start
        if (winningPanel != null)
        {
            winningPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Displays the winning panel and pauses the game.
    /// </summary>
    public void ShowWinningPanel()
    {
        if (winningPanel != null)
        {
            winningPanel.SetActive(true);

            // Mute or stop car sound
            if (carAudioSource != null)
            {
                carAudioSource.volume = 0f;
                carAudioSource.Stop();
            }

            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogWarning("Winning panel is not assigned in the Inspector.");
        }
    }

    /// <summary>
    /// Displays the game over panel and pauses the game.
    /// </summary>
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);

            // Mute or stop car sound
            if (carAudioSource != null)
            {
                carAudioSource.volume = 0f;
                carAudioSource.Stop();
            }

            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.LogWarning("Game over panel is not assigned in the Inspector.");
        }
    }

    /// <summary>
    /// Restarts the current level by resetting the player's car and game state.
    /// </summary>
    public void RestartLevel()
    {
        // Reset the car's position and rotation
        if (playerCar != null)
        {
            playerCar.transform.position = initialCarPosition;
            playerCar.transform.rotation = initialCarRotation;

            // Resume car audio
            if (carAudioSource != null)
            {
                carAudioSource.volume = 1f;
                carAudioSource.Play();
            }

            // Reset fuel
            if (fuelSystem != null)
            {
                fuelSystem.ResetFuel();
            }
        }
        else
        {
            Debug.LogError("Player car reference is missing!");
        }

        // Hide UI panels
        if (winningPanel != null)
        {
            winningPanel.SetActive(false);
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        // Reload the current scene to restart the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f; // Resume the game
    }

    /// <summary>
    /// Loads the next level based on the build index.
    /// </summary>
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume the game
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (fuelSystem != null)
            {
                fuelSystem.ResetFuel(); // Reset fuel when loading the next level
            }

            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more levels available. Returning to main menu.");
            LoadMainMenu();
        }
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Ensure "MainMenu" exists in Build Settings
    }

    /// <summary>
    /// Quits the game. Works only in a built application.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitting the game..."); // Logs in the editor for debugging
        Application.Quit(); // Works only in the build
    }
}
