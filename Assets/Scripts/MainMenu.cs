using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit(); //MainMenu Scene
    }
    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void SinglePlayerGame()
    {
        SceneManager.LoadScene(1); //Single Scene
    }
    public void MultiPlayerGame()
    {
        SceneManager.LoadScene(2); //Co-Op Scene
    }
}
