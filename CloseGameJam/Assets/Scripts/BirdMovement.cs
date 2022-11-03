using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] bool useLinearMovement;
    [SerializeField] bool useSmoothMovement;
    [SerializeField] bool usePhysicsMovement;
    [SerializeField] float speed;
    [SerializeField] Vector3 hurtPosition;
    public bool isMovingTowardsPosition;
    Vector3 targetPos;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        targetPos = hurtPosition;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        if (isMovingTowardsPosition)
        {
            ForcedMoveTowardsTargetPosition(targetPos, 4);
            if (Vector2.Distance(this.gameObject.transform.position, targetPos) < 0.10f)
            {
                isMovingTowardsPosition = false;
            }
        }
        else
        {
            if (useLinearMovement)
            {
                MoveTowardsTargetPosition(cam.ScreenToWorldPoint(mousePos));
            }
            else if (useSmoothMovement)
            {
                SmoothMoveTowardsTargetPosition(cam.ScreenToWorldPoint(mousePos));
            }
            else if (usePhysicsMovement)
            {
                //PhysicsMoveTowardsTargetPosition(cam.ScreenToWorldPoint(mousePos));
            }
            else
            {
                this.gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldpos = cam.ScreenToWorldPoint(mousePos);
        PhysicsMoveTowardsTargetPosition(worldpos);

        if (worldpos.x - this.transform.position.x < -0.2f)
        {
            //Flip it to be facing left
            this.transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Left");
        }
        if (worldpos.x - this.transform.position.x > 0.2f)
        {
            //Flip it to be facing right
            this.transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Right");
        }
    }

    private void ForcedMoveTowardsTargetPosition(Vector2 pos, float moveSpeed)
    {
        Vector3 targetDir = (Vector3)pos - this.transform.position;
        targetDir.z = 0;
        targetDir.Normalize();
        this.gameObject.transform.position += targetDir * Time.deltaTime * moveSpeed;
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
        if (dist > 0.05f)
        {
            this.gameObject.transform.position += targetDir * Time.deltaTime * speed * Mathf.Sqrt(Mathf.Max(dist, 1));
        }
    }

    private void PhysicsMoveTowardsTargetPosition(Vector2 pos)
    {
        Vector3 targetDir = (Vector3)pos - this.transform.position;
        float dist = targetDir.magnitude;
        targetDir.z = 0;
        targetDir.Normalize();
        if (dist > 0.15f)
        {
            rb2d.MovePosition(this.gameObject.transform.position + (targetDir * Time.deltaTime * speed * Mathf.Sqrt(Mathf.Max(dist, 1))));
        }
    }

}
