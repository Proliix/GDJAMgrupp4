using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] GameObject scoreBoardPanel;
    [SerializeField] GameObject blankButtonWithTextPrefab;

    // Start is called before the first frame update
    void Start()
    {
        int numScores = PlayerPrefs.GetInt("NumScores", 0);
        List<int> scores = new List<int>();
        for(int i = 0; i < numScores; i++)
        {
            scores.Add(PlayerPrefs.GetInt("Highscore"+i,0));
        }
        scores.Sort();
        scores.Reverse();
        for(int i = 0; i < Mathf.Min(scores.Count,4); i++)
        {
            GameObject buttonWithText = Instantiate(blankButtonWithTextPrefab, scoreBoardPanel.transform);
            buttonWithText.GetComponentInChildren<TextMeshProUGUI>().text = scores[i].ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
