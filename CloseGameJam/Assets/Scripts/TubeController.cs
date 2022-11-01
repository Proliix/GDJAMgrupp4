using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour
{
    public GameObject ToothPaste;
    public Transform SpawnPos;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.transform.position.y > gameObject.transform.position.y)
        {
            Instantiate(ToothPaste, SpawnPos.position, ToothPaste.transform.rotation);
        }
    }
}
