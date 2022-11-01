using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooth : MonoBehaviour
{
    [SerializeField] float totalbrushes;
    float brushesRemaining;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Start()
    {
        brushesRemaining = totalbrushes;
    }
    public void BrushTooth()
    {
        brushesRemaining--;
        Debug.Log(brushesRemaining);
        if(brushesRemaining <= 0)
        {
            spriteRenderer.color = Color.white;
        }
    }

    public bool IsBrushed()
    {
        if(brushesRemaining <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
