using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothManager : MonoBehaviour
{
    List<Tooth> toothList;
    [SerializeField] GameObject toothPrefab;
    [SerializeField] AligatorController aligatorController;
    
    public void InitializeTeeth(AligatorController aligatorController)
    {
        for(int i = 0; i < aligatorController.toothPositions.Length; i++)
        {
            GameObject toothObject = Instantiate(toothPrefab, aligatorController.toothPositions[i].position, aligatorController.toothPositions[i].rotation, aligatorController.toothPositions[i]);
            Tooth tooth = toothObject.GetComponent<Tooth>();
            toothList.Add(tooth);
            tooth.totalbrushes = Random.Range(0, 11);
            tooth.brushesRemaining = tooth.totalbrushes;
        }
    }
    private void Start()
    {
        InitializeTeeth(aligatorController);
    }

    private void Update()
    {
        bool allCleaned = true;
        for(int i = 0; i < toothList.Count; i++)
        {
            if (toothList[i].brushesRemaining > 0)
            {
                allCleaned = false;
                break;
            }
        }
        if(allCleaned)
        {
            Debug.Log("GAME IS OVER WIN");
        }
    }
}
