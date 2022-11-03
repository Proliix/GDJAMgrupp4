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


    float crocTimer;
    Vector3 startPos;
    GameObject currentCroc;
    Animator anim;
    ToothManager toothManager;
    bool isSendingOut = false;

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
        currentCroc = startCroc;
        startPos = currentCroc.transform.position;
        anim = currentCroc.GetComponent<Animator>();
        toothManager = gameObject.GetComponent<ToothManager>();
        crocTimer = crocTimeToLeave;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void ReloadScene()
    {
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



        if (Input.GetKeyDown(KeyCode.Escape))
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
