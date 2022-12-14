using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Crocodile")]
    public GameObject[] cocodilePrefabs;
    public GameObject startCroc;
    [Range(0, 60)]
    public float crocTimeToLeave = 15;
    public TextMeshProUGUI CrocTimerText;
    [Header("Pause")]
    public GameObject pauseObj;
    [Header("GameOver")]
    public GameObject gameOverObj;
    public GameObject timeoutObj;
    [Header("Day Night Cycle")]
    public GameObject ClockArrow;
    public GameObject Background;
    public float DayNightTime = 300;
    [Header("Extras")]
    [SerializeField] ScoreManager scoreManager;


    Health pHealth;
    float crocTimer;
    Vector3 startPos;
    [HideInInspector] public GameObject currentCroc;
    Animator anim;
    Animator clockArmAnim;
    Animator backgroundAnim;
    Animator soundAnim;
    AudioSource sound;
    ToothManager toothManager;
    bool isSendingOut = false;

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
        timeoutObj.SetActive(false);
        sound = gameObject.GetComponent<AudioSource>();
        clockArmAnim = ClockArrow.GetComponent<Animator>();
        backgroundAnim = Background.GetComponent<Animator>();
        soundAnim = gameObject.GetComponent<Animator>();
        clockArmAnim.SetFloat("TimeScale", 1 / DayNightTime);
        backgroundAnim.SetFloat("TimeScale", 1 / DayNightTime);
        pHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        gameOverObj.SetActive(false);
        currentCroc = startCroc;
        startPos = currentCroc.transform.position;
        anim = currentCroc.GetComponent<Animator>();
        toothManager = gameObject.GetComponent<ToothManager>();
        crocTimer = crocTimeToLeave;
    }

    public void LoadMenu()
    {
        scoreManager.AddScoreToHighscore();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ReloadScene()
    {
        scoreManager.AddScoreToHighscore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        pauseObj.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartShake()
    {
        StartCoroutine(Shake(0.2f, 0.1f));
    }

    IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 OriginalPos = Camera.main.transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.localPosition = new Vector3(x, y, OriginalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.localPosition = OriginalPos;

    }

    // Update is called once per frame
    void Update()
    {
        DayNightTime -= Time.deltaTime;


        if (DayNightTime <= 0)
        {
            if (sound.isPlaying)
                sound.Stop();

            Time.timeScale = 0;
            timeoutObj.SetActive(true);
        }

        if (DayNightTime <= 12 && !sound.isPlaying && !pHealth.isDead && DayNightTime > 0)
        {
            sound.Play();
            soundAnim.SetTrigger("StartSound");
        }


        if (crocTimer > 0)
        {
            crocTimer -= Time.deltaTime;
        }
        else if (crocTimer <= 0)
        {
            crocTimer = 0;
        }

        if (!isSendingOut)
            CrocTimerText.text = crocTimer.ToString("F2");


        if (pHealth.isDead)
        {
            Time.timeScale = 0;
            gameOverObj.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !pHealth.isDead)
        {
            Time.timeScale = 0;
            pauseObj.SetActive(true);
        }

        if (!isSendingOut && toothManager.doneCleaning || !isSendingOut && crocTimer <= 0)
        {
            SendOutCocodile();
        }
    }

    private void SendOutCocodile()
    {
        isSendingOut = true;
        anim.SetTrigger("Remove");
        anim.SetBool("isLeaving", true);
        Destroy(currentCroc, 4);
        StartCoroutine(SendInNewCrocodile());
    }

    public IEnumerator SendInNewCrocodile()
    {
        yield return new WaitForSeconds(4);
        int index = Random.Range(0, cocodilePrefabs.Length);
        currentCroc = Instantiate(cocodilePrefabs[index], startPos, cocodilePrefabs[index].transform.rotation);
        anim = currentCroc.GetComponent<Animator>();
        anim.SetBool("isLeaving", false);
        toothManager.InitializeTeeth(currentCroc.GetComponent<AligatorController>());
        yield return new WaitForSeconds(1);
        crocTimer = crocTimeToLeave;
        isSendingOut = false;
    }
}
