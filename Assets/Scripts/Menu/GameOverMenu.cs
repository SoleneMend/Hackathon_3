using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool IsGameOver = false;

    [Header("UI")]
    public GameObject gameOverUI;

    void Start()
    {
        gameOverUI.SetActive(false);
    }


    public void GameOver()
    {
        IsGameOver = true;

        gameOverUI.SetActive(true);

        Time.timeScale = 0f;
    }


    public void Restart()
    {
        IsGameOver = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }


    public void BackMainMenu()
    {
        IsGameOver = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }


    public void Exit()
    {
        Debug.Log("Quitter le jeu");

        Application.Quit();

    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }
}
