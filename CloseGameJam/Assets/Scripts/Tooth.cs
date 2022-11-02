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
        UpdateToothLook();
    }
    public void BrushTooth()
    {
        brushesRemaining--;
        Debug.Log(brushesRemaining);
        UpdateToothLook();
    }

    private void UpdateToothLook()
    {
        if (brushesRemaining <= 0)
        {
            scoreManager.AddScore();
            spriteRenderer.sprite = cleanedToothSprite;
            return;
        }
        if (brushesRemaining <= 6)
        {
            spriteRenderer.sprite = halfCleanedToothSprite;
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
