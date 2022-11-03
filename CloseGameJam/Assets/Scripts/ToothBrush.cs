using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToothBrush : MonoBehaviour
{
    [SerializeField] List<AudioClip> clips;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float pasteRemaining;
    [SerializeField] float maxPaste = 100;
    [SerializeField] GameObject pasteArt;

    float startPitch;

    private void Start()
    {
        startPitch = audioSource.pitch;
    }

    private void Update()
    {
        pasteArt.transform.localScale = new Vector3(0.5f + 0.5f * (pasteRemaining / maxPaste), (pasteRemaining / maxPaste), 0);
    }

    public void AddPaste(float ammount)
    {
        if (pasteRemaining + ammount >= maxPaste)
        {
            pasteRemaining = maxPaste;
        }
        else
            pasteRemaining += ammount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger enter");
        if (collision.gameObject.GetComponent<Tooth>() != null)
        {
            if (pasteRemaining <= 0)
            {
                Debug.Log("No paste left");
                return;
            }
            if (!collision.gameObject.GetComponent<Tooth>().IsBrushed())
            {
                pasteRemaining--;
                collision.gameObject.GetComponent<Tooth>().BrushTooth();
                audioSource.pitch = Random.Range(startPitch - 0.25f, startPitch + 0.25f);
                audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
                Debug.Log(collision.gameObject.name + " was brushed");
            }
        }
    }
}
