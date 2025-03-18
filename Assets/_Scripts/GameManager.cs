using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using DG.Tweening;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private int score=0;
    [SerializeField] private List<Hearts> heartsUI;
    [SerializeField] ScoreUI scoreUICounter;


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
        CameraShake.Shake(0.2f, 0.3f);
        currentBrickCount--;
        IncreaseScore();

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
        // update lives on HUD here
        // game over UI if maxLives < 0, then exit to main menu after delay
        CameraShake.Shake(0.6f, 0.7f);
        ball.ResetBall();
    }

    private IEnumerator GameOverRoutine()
    {
        Debug.Log("GameOverRoutine started.");

        // ✅ Allow animations to play first before delaying
        yield return new WaitForSeconds(0.5f); // Adjust if needed to fit heart animation duration

        Debug.Log("GameOver delay started. Waiting for 3 seconds before freeze...");

        yield return new WaitForSecondsRealtime(0.5f); // ⏳ Now delay AFTER animations

        Debug.Log("GameOver delay completed. Freezing game.");

        Time.timeScale = 0f; // ✅ Freeze AFTER all animations finish

        yield return new WaitForSecondsRealtime(1.5f); // Small buffer before transitioning (optional)

        Time.timeScale = 1f; // Restore time scale before switching scenes

        if (SceneHandler.Instance != null)
        {
            Debug.Log("Loading menu scene...");
            SceneHandler.Instance.LoadMenuScene();
        }
        else
        {
            Debug.LogError("SceneHandler.Instance is null! Scene transition failed.");
        }
    }

    private void UpdateHeartsUI()
    {
        
        Debug.Log($"Updating hearts UI - Current Lives: {currentLives}");
        int lastHeartIndex = currentLives;
        for (int i = 0; i < heartsUI.Count; i++)
        {
            bool isFull = i < currentLives;
            Debug.Log($"Setting heart {i} to {(isFull ? "FULL" : "EMPTY")}");
            if (!isFull) {
             
                Hearts heart = heartsUI[i];
                if (heart == null) continue;
                heart.transform.DOShakeScale(0.3f, 0.5f, 10).OnComplete(() => {
                    heart.GetComponent<CanvasGroup>().DOFade(0, 0.2f).OnComplete(() => {
                        heart.setFull(false);
                        heart.transform.DOScale(Vector3.one * 0.5f, 0.2f);
                        heart.GetComponent<CanvasGroup>().DOFade(1, 0.2f).OnComplete(() =>
                        {
                            if (i == lastHeartIndex)
                            {
                                Debug.Log("Last heart animation done, triggering game freeze.");
                                StartCoroutine(GameOverRoutine());

                            }
                        });
                    });
                });
            }
            else
            {
                heartsUI[i].setFull(isFull);
                heartsUI[i].transform.DOScale(Vector3.one * 0.5f, 0.2f);

            }


           
        }
        if (currentLives <= 0)
        {
            StartCoroutine(GameOverRoutine());

        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreUICounter.UpdateScore(score);
    }

}
