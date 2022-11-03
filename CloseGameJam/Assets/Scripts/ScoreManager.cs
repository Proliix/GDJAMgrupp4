using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;

    [Header("UI Elements")]
    public TextMeshProUGUI ScoreText;

    public void AddScore(int addScore = 5)
    {
        score += addScore;
    }

    private void Update()
    {
        ScoreText.text = "" + score;
    }

}
