using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooth : MonoBehaviour
{
    public float totalbrushes;
    public float brushesRemaining;
    public SpriteRenderer spriteRenderer;
    [SerializeField] Sprite dirtyToothSprite;
    [SerializeField] Sprite halfCleanedToothSprite;
    [SerializeField] Sprite cleanedToothSprite;

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
        if (brushesRemaining <= 6)
        {
            scoreManager.AddScore();
            spriteRenderer.sprite = halfCleanedToothSprite;
            return;
        }
        if(brushesRemaining <= 0)
        {
            scoreManager.AddScore();
            spriteRenderer.sprite = cleanedToothSprite;
            return;
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
