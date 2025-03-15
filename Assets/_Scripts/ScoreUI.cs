using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;

public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private TextMeshProUGUI toUpdate;
    [SerializeField] private Transform ScoreTextContainer;
    [SerializeField] private float duration=0.4f;
    [SerializeField] private Ease animationCurve;

    private float containerInitPosition;
    private float moveAmount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Canvas.ForceUpdateCanvases();
        current.SetText("0");
        toUpdate.SetText("0");
        containerInitPosition = ScoreTextContainer.localPosition.y;
        moveAmount = current.rectTransform.rect.height;

        


    }
    public void UpdateScore(int score) {
        toUpdate.SetText($"{score}");
        ScoreTextContainer.DOLocalMoveY(containerInitPosition + moveAmount, duration).SetEase(animationCurve);
        StartCoroutine(ResetScoreContainer(score));
    }

    private IEnumerator ResetScoreContainer(int score)
    {
        yield return new WaitForSeconds(duration);
        current.SetText($"{score}");
        Vector3 localPosition = ScoreTextContainer.localPosition;
        ScoreTextContainer.localPosition = new Vector3(localPosition.x,containerInitPosition,localPosition.z);



    }

    // Update is called once per frame
    
}
