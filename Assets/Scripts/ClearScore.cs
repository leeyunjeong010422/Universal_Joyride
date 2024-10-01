using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalScoreText;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            int totalScore = GameManager.Instance.totalPoint;
            totalScoreText.text = "Total Score: " + totalScore.ToString();
        }
    }
}
