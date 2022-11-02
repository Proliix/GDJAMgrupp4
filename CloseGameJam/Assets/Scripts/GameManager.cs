using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Crocodile")]
    public GameObject cocodilePrefab;
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

    private void ChangeSendOutBool(bool value = false)
    {
        isSendingOut = value;
    }

    public IEnumerator SendInNewCrocodile()
    {
        yield return new WaitForSeconds(4);
        currentCroc = Instantiate(cocodilePrefab, startPos, cocodilePrefab.transform.rotation);
        anim = currentCroc.GetComponent<Animator>();
        toothManager.InitializeTeeth(currentCroc.GetComponent<AligatorController>());
        yield return new WaitForSeconds(1);
        ChangeSendOutBool(true);
    }
}
