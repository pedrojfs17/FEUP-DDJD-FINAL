using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Transform playerInFront;
    
    public bool inFront = false;

    float speed = 10.0f;

    const float EPSILON = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inFront) {
            /* if (Input.GetKey(KeyCode.DownArrow)) {
                transform.Rotate(90.0f * Time.deltaTime, 0.0f, 0.0f, Space.Self);
            } else if (Input.GetKey(KeyCode.UpArrow)) {
                transform.Rotate(-90.0f * Time.deltaTime, 0.0f, 0.0f, Space.Self);
            }  */

            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
        } 
        else {
            transform.LookAt(playerInFront.position);

            if ((transform.position - playerInFront.position).magnitude > EPSILON) {
                transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
            }
        }
    }
}
