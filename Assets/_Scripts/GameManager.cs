using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private float score=0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private List<Hearts> heartsUI;


    private int currentBrickCount;
    private int totalBrickCount;
    private int currentLives;
    
    

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
        currentLives = maxLives;
        score = 0;
        UpdateHeartsUI();
       
    }

    private void OnDisable()
    {
        InputHandler.Instance.OnFire.RemoveListener(FireBall);
    }

    private void FireBall()
    {
        ball.FireBall();
    }

    public void OnBrickDestroyed(Vector3 position)
    {
        // fire audio here
        // implement particle effect here
        // add camera shake here
        currentBrickCount--;
        score++;
        scoreText.text = $"Score:{score}";
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        Debug.Log($"Current Score: {score}");
        if (currentBrickCount <= 0) {
            score = 0;
            SceneHandler.Instance.LoadNextScene(); 
            }
    }

    public void KillBall()
    {
        currentLives--;
        Debug.Log($"Current Lives: {maxLives}");
        UpdateHeartsUI();
        if (currentLives <= 0)
        {
            StartCoroutine(GameOverRoutine());
            
        }
        else {
            ball.ResetBall();
        }
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
       
    }

    private IEnumerator GameOverRoutine()
    {
        Debug.Log("GameOverRoutine started.");

        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1.8f);
        Time.timeScale = 1f;

        Debug.Log("Coroutine finished waiting, trying to switch scenes.");

        if (SceneHandler.Instance != null)
        {
            SceneHandler.Instance.LoadMenuScene();
            Debug.Log("LoadMenuScene was called.");
        }
        else
        {
            Debug.LogError("SceneHandler.Instance is null. Scene transition failed.");
        }
    }
    private void UpdateHeartsUI()
    {
        Debug.Log($"Updating hearts UI - Current Lives: {currentLives}");
        for (int i = 0; i < heartsUI.Count; i++)
        {
            bool isFull = i < currentLives;
            Debug.Log($"Setting heart {i} to {(isFull ? "FULL" : "EMPTY")}");
            heartsUI[i].setFull(isFull);
        }
    }

}
