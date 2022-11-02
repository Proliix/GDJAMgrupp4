using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Crocodile")]
    public GameObject[] cocodilePrefabs;
    public GameObject startCroc;

    Vector3 startPos;
    GameObject currentCroc;
    Animator anim;
    ToothManager toothManager;
    bool isSendingOut = false;

    // Start is called before the first frame update
    void Start()
    {
        currentCroc = startCroc;
        startPos = currentCroc.transform.position;
        anim = currentCroc.GetComponent<Animator>();
        toothManager = gameObject.GetComponent<ToothManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSendingOut && toothManager.doneCleaning)
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
        isSendingOut = false;
    }
}
