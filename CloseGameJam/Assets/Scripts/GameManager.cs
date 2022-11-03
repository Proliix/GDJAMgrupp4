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
    [Header("Day Night Cycle")]
    public GameObject ClockArrow;
    public GameObject Background;
    public float DayNightTime = 300;
    [Header("Extras")]
    [SerializeField] ScoreManager scoreManager;


    Health pHealth;
    float crocTimer;
    Vector3 startPos;
    GameObject currentCroc;
    Animator anim;
    Animator clockArmAnim;
    Animator backgroundAnim;
    ToothManager toothManager;
    bool isSendingOut = false;

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
        clockArmAnim = ClockArrow.GetComponent<Animator>();
        backgroundAnim = Background.GetComponent<Animator>();
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



    // Update is called once per frame
    void Update()
    {
        DayNightTime -= Time.deltaTime;

        if (DayNightTime <= 0)
        {
            pHealth.isDead = true;
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
        Destroy(currentCroc, 4);
        StartCoroutine(SendInNewCrocodile());
    }

    public IEnumerator SendInNewCrocodile()
    {
        yield return new WaitForSeconds(4);
        int index = Random.Range(0, cocodilePrefabs.Length);
        currentCroc = Instantiate(cocodilePrefabs[index], startPos, cocodilePrefabs[index].transform.rotation);
        anim = currentCroc.GetComponent<Animator>();
        toothManager.InitializeTeeth(currentCroc.GetComponent<AligatorController>());
        yield return new WaitForSeconds(1);
        crocTimer = crocTimeToLeave;
        isSendingOut = false;
    }
}
