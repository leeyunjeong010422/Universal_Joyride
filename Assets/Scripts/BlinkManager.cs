using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Âü°í: https://maintaining.tistory.com/entry/Unity-UI-%EA%B9%9C%EB%B9%A1%EC%9E%84-%ED%9A%A8%EA%B3%BC-%EB%84%A3%EA%B8%B0
public class BlinkManager : MonoBehaviour
{
    public LoopType loopType;
    public TextMeshProUGUI text;

    private void Start()
    {
        text.DOFade(0.0f, 0.7f).SetLoops(-1, loopType);
    }
}
