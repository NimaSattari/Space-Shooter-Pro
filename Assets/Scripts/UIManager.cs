using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text ScoreText;
    [SerializeField] private Sprite[] LiveSprites;
    [SerializeField] private Image LivesImg;
    [SerializeField] private Text GameOverText;
    private GameManager GameManager;
    void Start()
    {
        ScoreText.text = "Score:" + 0;
        GameOverText.gameObject.SetActive(false);
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if(GameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }
    public void UpdateScore(int playerScore)
    {
        ScoreText.text = "Score: " + playerScore.ToString();
    }
    public void UpdateLives(int currentLives)
    {
        LivesImg.sprite = LiveSprites[currentLives];
        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        GameManager.GameOver();
        GameOverText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            GameOverText.text = "Game Over";
            yield return new WaitForSeconds(1f);
            GameOverText.text = "";
            yield return new WaitForSeconds(1f);
        }
    }
    public void ResumePlay()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ResumeGame();
    }
}
