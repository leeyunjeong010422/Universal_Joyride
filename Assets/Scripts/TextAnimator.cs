using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

//참고: https://ruyagames.tistory.com/m/23
//inputString: 최종적으로 표시될 문장
//duration : 문장이 모두 표시될때까지 걸리는 시간
//ease : 변환시의 시간당 변화량 그래프를 설정
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