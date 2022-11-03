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
    [SerializeField] GameObject cleanedParticles;
    [SerializeField] GameObject brushParticles;

    ScoreManager scoreManager;

    private void Start()
    {
        brushesRemaining = totalbrushes;
        scoreManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ScoreManager>();
        SetToothLook();
    }
    public void BrushTooth()
    {
        brushesRemaining--;
        Destroy(Instantiate(brushParticles, this.gameObject.transform.position, Quaternion.identity),2f);
        UpdateToothLook();
    }

    private void UpdateToothLook()
    {
        if (brushesRemaining <= 0)
        {
            scoreManager.AddScore();
            spriteRenderer.sprite = cleanedToothSprite;
            Destroy(Instantiate(cleanedParticles, this.gameObject.transform.position, Quaternion.identity), 2f);
            return;
        }
        if (brushesRemaining <= 6)
        {
            spriteRenderer.sprite = halfCleanedToothSprite;
            return;
        }
    }

    private void SetToothLook()
    {
        if (brushesRemaining <= 0)
        {
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
