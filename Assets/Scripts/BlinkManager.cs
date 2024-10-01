using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkManager : MonoBehaviour
{
    public LoopType loopType;
    public TextMeshProUGUI text;

    private void Start()
    {
        text.DOFade(0.0f, 0.7f).SetLoops(-1, loopType);
    }
}
