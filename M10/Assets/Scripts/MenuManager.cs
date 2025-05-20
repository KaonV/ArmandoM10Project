using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private void Update()
    {
       // CreditsExit();
       // GameplayExit();
    }

    public void Gameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void GameplayExit()
    {
       
            SceneManager.LoadScene("Menu");
        
    }


    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void CreditsExit()
    {
        if (SceneManager.GetActiveScene().name == "Credits" && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void Menu()
    {
            SceneManager.LoadScene("Menu");
        
    }

    public void Exit()
    {
        Application.Quit();
    }
}
