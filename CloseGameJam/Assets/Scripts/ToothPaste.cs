using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothPaste : MonoBehaviour
{
    
    public float pasteAmmount = 5;
    private ToothBrush toothBrush;

    void Start()
    {
        toothBrush = GameObject.FindGameObjectWithTag("ToothBrush").GetComponent<ToothBrush>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ToothBrush"))
        {
            toothBrush.pasteRemaining += pasteAmmount;
            Destroy(this.gameObject);
        }
    }
}
