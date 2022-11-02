using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothManager : MonoBehaviour
{
    List<Tooth> toothList;
    [SerializeField] GameObject toothPrefab;
    
    public void InitializeTeeth(AligatorController aligatorController)
    {
        for(int i = 0; i < aligatorController.toothPositions.Length; i++)
        {
            GameObject toothObject = Instantiate(toothPrefab, aligatorController.toothPositions[i].position, aligatorController.toothPositions[i].rotation);
            Tooth tooth = toothObject.GetComponent<Tooth>();
            toothList.Add(tooth);
            tooth.totalbrushes = Random.Range(0, 11);
        }
    }
}
