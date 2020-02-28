using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isGameOver;
    public bool isCoopMode = false;
    [SerializeField] private GameObject PauseMenuPanel;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isGameOver == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Current Game scene
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void GameOver()
    {
        isGameOver = true;
    }
    public void ResumeGame()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
