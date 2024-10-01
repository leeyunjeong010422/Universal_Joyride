using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    private Text targetText;
    public string inputString;
    public float duration;
    public Ease ease;

    private void Start()
    {
        targetText = GetComponent<Text>();
        targetText.text = "";
        targetText.DOText(inputString, duration).SetEase(ease);
    }
}