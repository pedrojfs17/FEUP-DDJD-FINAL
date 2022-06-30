using System;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    private Transform target;

    private float moveSpeed = 3.0f;
    private float rotationSpeed = 3.0f;

    private bool moving = false;
    private bool rotating = false;

    public void SetTarget(Transform targetTransform, bool startByRotating)
    {
        target = targetTransform;
        rotationSpeed = startByRotating ? 120.0f : 3.0f;

        moving = true;
        rotating = true;
    }

    private void EndMovement()
    {
        moving = false;
        transform.position = target.position;
        rotationSpeed = 120.0f;
    }

    private void EndRotation()
    {
        rotating = false;
        transform.rotation = target.rotation;
        rotationSpeed = 3.0f;
    }
 
    private void Update()
    {
        if (!moving && !rotating) return;

        Rotate();
        Move();
    }

    private void Rotate()
    {
        if (!rotating) return;

        float step = rotationSpeed * Time.deltaTime;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);

        if (transform.rotation == target.rotation) EndRotation();
    }

    private void Move()
    {
        if (!moving) return;
        
        float step = moveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if ((transform.position - target.position).magnitude < 0.2f) EndMovement();
    }
}
