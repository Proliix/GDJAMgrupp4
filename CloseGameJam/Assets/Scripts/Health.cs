using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] GameObject birdAnimObj;
    [Header("UI")]
    public GameObject healthImage;
    public Sprite normalHp;
    public Sprite DepleatedHp;
    public float imageGap = 10;

    GameObject[] healthImages;
    Animator birdAnim;
    BirdMovement movement;
    [HideInInspector]public bool isDead = false;
    CircleCollider2D playerCol;

    private void Start()
    {
        healthImages = new GameObject[health];
        healthImages[0] = healthImage;
        for (int i = 1; i < health; i++)
        {
            Vector3 pos = new Vector3(healthImages[i - 1].transform.position.x + imageGap, healthImages[i - 1].transform.position.y, healthImages[i - 1].transform.position.z);
            GameObject img = Instantiate(healthImage, pos, healthImage.transform.rotation, healthImage.transform.parent);
            healthImages[i] = img;
        }
        playerCol = gameObject.GetComponent<CircleCollider2D>();
        movement = gameObject.GetComponent<BirdMovement>();
        birdAnim = birdAnimObj.GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerCol.enabled == false && movement.isMovingTowardsPosition == false)
        {
            birdAnim.SetTrigger("Return");
            playerCol.enabled = true;
        }
    }


    public void TakeDamage()
    {
        if (movement.isMovingTowardsPosition == false)
        {
            birdAnim.SetTrigger("TakeDamage");
            health--;
            for (int i = 0; i < healthImages.Length; i++)
            {
                if (i > health - 1)
                {
                    healthImages[i].GetComponent<RawImage>().texture = DepleatedHp.texture;
                }
            }

            movement.isMovingTowardsPosition = true;
            playerCol.enabled = false;
            if (health <= 0)
            {
                isDead = true;
            }
        }
    }



}
