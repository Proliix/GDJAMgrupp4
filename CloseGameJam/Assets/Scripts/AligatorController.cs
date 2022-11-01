using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AligatorController : MonoBehaviour
{
    public GameObject HurtBox;
    [Header("Timer")]
    public float timeToShake = 5;
    [Tooltip("Reset time is timeToShake + timeToClose")]
    public float timeToClose = 2.5f;
    [Tooltip("Reset time is timeToClose + resetTime")]
    public float resetTime = 3f;


    private bool isShaking = false;
    private bool hasClosed = false;
    private Animator anim;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToShake && !isShaking)
        {
            anim.SetTrigger("StartShake");
            isShaking = true;
        }
        else if (timer >= (timeToShake + timeToClose) && !hasClosed)
        {
            anim.SetTrigger("CloseMouth");
            hasClosed = true;
        }
        else if (timer >= ((timeToShake + timeToClose) + resetTime))
        {
            timer = 0;
            anim.SetTrigger("Restart");
            isShaking = false;
            hasClosed = false;
        }
    }
}
