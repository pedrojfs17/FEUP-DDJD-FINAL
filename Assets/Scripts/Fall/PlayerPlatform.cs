using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatform : MonoBehaviour
{

    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 20;
    public int points = 0;
    public bool cangetmore = true;

    // Start is called before the first frame update
    void Start()
    {
        Transform transform = GetComponent<Transform>();
        if(transform.position.y < 1.1){
            Destroy(gameObject);
        }
        
    }

    public void MoreHealth(){
        if (cangetmore == true){
            points += 10;
            UnityEngine.Debug.Log("kaka");
        }
    }

    public int getHealth(){
        return points;
    }

    public bool getCanGetMore(){
        return cangetmore;
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = GetComponent<Transform>();
        if(transform.position.y < 0){
            cangetmore = false;
        }
        else{
            cangetmore = true;
        }
        /*if(Input.GetKey("d")){
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
        }*/
        
    }

    public void IsStopped(){
        rb.velocity = Vector3.zero;
    }

    public void GoLeft(){
        rb.AddForce(0,0,-sidewaysForce * 750 * Time.deltaTime);
    }
    public void GoRight(){
        rb.AddForce(0,0,sidewaysForce * 750 * Time.deltaTime);
    }
    public void GoUp(){
        rb.AddForce(-sidewaysForce * 750 * Time.deltaTime,0,0);
    }
    public void GoDown(){
        rb.AddForce(sidewaysForce * 750 * Time.deltaTime,0,0);
    }

   
}
