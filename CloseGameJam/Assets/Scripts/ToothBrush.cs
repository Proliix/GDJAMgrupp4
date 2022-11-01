using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothBrush : MonoBehaviour
{

    [SerializeField] float pasteRemaining;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter");
        if(collision.gameObject.GetComponent<Tooth>() != null)
        {
            if(pasteRemaining <= 0)
            {
                Debug.Log("No paste left");
                return;
            }
            if (!collision.gameObject.GetComponent<Tooth>().IsBrushed())
            {
                pasteRemaining--;
                collision.gameObject.GetComponent<Tooth>().BrushTooth();
                Debug.Log(collision.gameObject.name + " was brushed");
            }
        }
    }
}
