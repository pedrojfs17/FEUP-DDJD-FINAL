using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{

    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 20;
    public float health = 50f;

    // Start is called before the first frame update
    void Start()
    {
        Transform transform = GetComponent<Transform>();
        if(transform.position.y < 1.1){
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("d")){
            rb.AddForce(0,0,sidewaysForce * 100 * Time.deltaTime);
        }
        else if(Input.GetKey("a")){
            rb.AddForce(0,0,-sidewaysForce * 100 * Time.deltaTime);
        }
        else if(Input.GetKey("w")){
            rb.AddForce(-sidewaysForce * 100 * Time.deltaTime,0,0);
        }
        else if(Input.GetKey("s")){
            rb.AddForce(sidewaysForce * 100 * Time.deltaTime,0,0);
        }
        else{
            rb.velocity = Vector3.zero;
        }
        
    }

   
}
