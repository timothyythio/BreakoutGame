using TMPro;
using UnityEngine;
using DG.Tweening;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private float delayBeforeReturn = 3f;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip gameOverSound;


    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private Ease fadeEase = Ease.InOutSine;

    private void Start()
    {
        PlayGameOverSound();
        PlayGameOverAnimation();
        Invoke(nameof(ReturnToMainMenu), delayBeforeReturn);

    }

    private void PlayGameOverAnimation()
    {
        // Start with black color
        gameOverText.color = Color.black;



        // Fade from black to red
        gameOverText.DOColor(Color.red, fadeDuration).SetEase(fadeEase);

    }

    private void PlayGameOverSound()
    {
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    private void ReturnToMainMenu()
    {
        gameOverText.DOColor(Color.black, fadeDuration).SetEase(fadeEase)
                    .OnComplete(() =>
                    {
                        SceneHandler.Instance.LoadMenuScene();
                    });
    }

}

