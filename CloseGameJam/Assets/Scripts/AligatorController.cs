using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AligatorController : MonoBehaviour
{
    [Header("Teeth")]
    public Transform[] toothPositions;
    [Range(0, 1)]
    public float chanceForTooth;
    [Range(0, 1)]
    public float cleanedChance;
    public Vector2 teethHealth;
    [Header("Timer")]
    public Vector2 timeToShakeRange;
    [Tooltip("Reset time is timeToShake + timeToClose")]
    public Vector2 timeToCloseRange;
    [Tooltip("Reset time is timeToClose + resetTime")]
    public Vector2 timeToResetRange;

    private GameManager gameManager;
    private AudioSource audioSource;
    private float timeToShake = 5;
    private float timeToClose = 2.5f;
    private float resetTime = 3f;
    private bool isShaking = false;
    private bool hasClosed = false;
    private Animator anim;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        anim = gameObject.GetComponent<Animator>();
        timeToShake = Random.Range(timeToShakeRange.x, timeToShakeRange.y);
        timeToClose = Random.Range(timeToCloseRange.x, timeToCloseRange.y);
        resetTime = Random.Range(timeToResetRange.x, timeToResetRange.y);
        audioSource = gameObject.GetComponent<AudioSource>();

    }

    public void PlayBiteSound()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void StartCameraShake()
    {
        gameManager.StartShake();
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
