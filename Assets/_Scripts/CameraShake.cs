using UnityEngine;
using DG.Tweening;  

public class CameraShake : MonoBehaviour
{
    private static CameraShake _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void Shake(float duration = 0.2f, float strength = 0.3f)
    {
        if (_instance != null)
        {
            _instance.transform.DOShakePosition(duration, strength);
        }
    }
}
