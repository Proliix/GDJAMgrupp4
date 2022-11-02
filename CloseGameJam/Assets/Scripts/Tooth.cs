using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooth : MonoBehaviour
{
    [SerializeField] float totalbrushes;
    float brushesRemaining;
    [SerializeField] SpriteRenderer spriteRenderer;

    ScoreManager scoreManager;

    private void Start()
    {
        brushesRemaining = totalbrushes;
        scoreManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
    }
    public void BrushTooth()
    {
        brushesRemaining--;
        Debug.Log(brushesRemaining);
        if (brushesRemaining <= 0)
        {
            scoreManager.AddScore();
            spriteRenderer.color = Color.white;
        }
    }

    public bool IsBrushed()
    {
        if (brushesRemaining <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
