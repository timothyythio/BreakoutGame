using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip bounceEffect;
    [SerializeField] private AudioClip breakEffect;
    [SerializeField] public AudioSource bgmSource;
    [SerializeField] public AudioSource bounceSource;
    [SerializeField] public AudioSource breakSource;
    [SerializeField] public Ball ball;


    private void Update()
    {
        ball.ballBounced.AddListener(playBounceSFX);
    }
    public void playBGM()
    {
        bgmSource.clip = backgroundMusic;
        bgmSource.Play();
    }

    public void playBounceSFX()
    {
        bounceSource.clip = bounceEffect;
        bounceSource.Play();
    }

    public void playBreakSFX()
    {
        breakSource.clip = breakEffect;
        breakSource.Play();
    }
}
