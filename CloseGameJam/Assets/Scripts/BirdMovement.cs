using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] bool useLinearMovement;
    [SerializeField] bool useSmoothMovement;
    [SerializeField] float speed;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
       
        if(useLinearMovement)
        {
            MoveTowardsTargetPosition(cam.ScreenToWorldPoint(mousePos));
        }
        else if(useSmoothMovement)
        {
            SmoothMoveTowardsTargetPosition(cam.ScreenToWorldPoint(mousePos));
        }
        else
        {
            this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    private void MoveTowardsTargetPosition(Vector2 pos)
    {
        Vector3 targetDir = (Vector3)pos - this.transform.position;
        targetDir.z = 0;
        targetDir.Normalize();
        this.gameObject.transform.position += targetDir * Time.deltaTime * speed;
    }
    private void SmoothMoveTowardsTargetPosition(Vector2 pos)
    {
        Vector3 targetDir = (Vector3)pos - this.transform.position;
        float dist = targetDir.magnitude;
        targetDir.z = 0;
        targetDir.Normalize();
        if(dist > 0.05f)
        {
            this.gameObject.transform.position += targetDir * Time.deltaTime * speed * Mathf.Sqrt(Mathf.Max(dist,1));
        }
    }

}
