using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.LoadScene("Level1"); // Replace "Level1" with your first level's scene name
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
