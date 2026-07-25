using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Bouton Start
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    // Bouton Settings
    public void OpenSettings()
    {
        Debug.Log("Menu des paramètres");
        // Exemple :
        // settingsPanel.SetActive(true);
        // ou SceneManager.LoadScene("Settings");
    }

    // Bouton Exit
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}