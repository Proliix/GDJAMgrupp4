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

    public void AddScoreToHighscore(int scoreToAdd)
    {
        int numScores = PlayerPrefs.GetInt("NumScores", 0);
        PlayerPrefs.SetInt("NumScores", numScores + 1);
        PlayerPrefs.SetInt("Highscore" + numScores, scoreToAdd);
    }
    public void AddScoreToHighscore()
    {
        int numScores = PlayerPrefs.GetInt("NumScores", 0);
        PlayerPrefs.SetInt("NumScores", numScores + 1);
        PlayerPrefs.SetInt("Highscore" + numScores,score);
    }
}
