using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Transform playerInFront;
    public int playerInBackIndex;
    
    public bool inFront = false;

    float speed = 10.0f;

    const float EPSILON = 2.4f;

    // Update is called once per frame
    void Update()
    {
        if (inFront) {
            transform.LookAt(transform.position + new Vector3(10, 0, 0));
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            return;
        } 

        float step = speed * Time.deltaTime;

        Quaternion lookRotation = Quaternion.LookRotation(playerInFront.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, step);

        if ((transform.position - playerInFront.position).magnitude > EPSILON) {
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        }
    }
}
