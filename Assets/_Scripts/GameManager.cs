using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : SingletonMonoBehavior<GameManager>
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Ball ball;
    [SerializeField] private Transform bricksContainer;
    [SerializeField] private float score=0;
    [SerializeField] private float lives = 0;

    private int currentBrickCount;
    private int totalBrickCount;

    private void OnEnable()
    {
        InputHandler.Instance.OnFire.AddListener(FireBall);
        ball.ResetBall();
        totalBrickCount = bricksContainer.childCount;
        currentBrickCount = bricksContainer.childCount;
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
        Debug.Log($"Destroyed Brick at {position}, {currentBrickCount}/{totalBrickCount} remaining");
        Debug.Log($"Current Score: {score}");
        if (currentBrickCount <= 0) {
            score = 0;
            SceneHandler.Instance.LoadNextScene(); 
            }
    }

    public void KillBall()
    {
        maxLives--;
        Debug.Log($"Current Lives: {maxLives}");
        if (maxLives <= 0)
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
        yield return new WaitForSecondsRealtime(1.5f);
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

}
