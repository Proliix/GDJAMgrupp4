using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject deathScreen;

    BirdMovement movement;
    bool isDead = false;
    CircleCollider2D playerCol;

    private void Start()
    {
        playerCol = gameObject.GetComponent<CircleCollider2D>();
        deathScreen.SetActive(false);
        movement = gameObject.GetComponent<BirdMovement>();
    }

    private void Update()
    {
        if (playerCol.enabled == false && movement.isMovingTowardsPosition == false)
        {
            playerCol.enabled = true;
        }
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void TakeDamage()
    {
        if (movement.isMovingTowardsPosition == false)
        {
            Debug.Log("Took damage");
            health--;
            movement.isMovingTowardsPosition = true;
            playerCol.enabled = false;
            if (health <= 0)
            {
                deathScreen.SetActive(true);
                isDead = true;
                Invoke(nameof(ReturnToMenu), 5);
            }
        }
    }



}
