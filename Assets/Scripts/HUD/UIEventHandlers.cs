using UnityEngine;
using UnityEngine.SceneManagement;

public class UIEventHandlers : MonoBehaviour
{
    
    
    public void ExitGameButton()
    {
        // Salir de la aplicación.
        Application.Quit();
    }

    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    
}
