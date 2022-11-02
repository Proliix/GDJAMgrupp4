using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Crocodile")]
    public GameObject cocodilePrefab;



    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SendInNewCrocodile();
    }

    public void SendInNewCrocodile()
    {
        
    }
}
