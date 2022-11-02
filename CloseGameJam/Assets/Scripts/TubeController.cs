using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    public GameObject ToothPaste;
    public Transform SpawnPos;
    public Sprite NormalTube;
    public Sprite squishedTube;

    private SpriteRenderer sr;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        sr = gameObject.GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.transform.position.y > gameObject.transform.position.y)
        {
            sr.sprite = squishedTube;
            GameObject obj = Instantiate(ToothPaste, SpawnPos.position, ToothPaste.transform.rotation);
            Destroy(obj, 5);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sr.sprite = NormalTube;
        }
    }
}
