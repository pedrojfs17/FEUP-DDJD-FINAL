using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 20;
    public float health = 10;
    public float dir = 1;
    public string right;
    public string left;

    void Start()
    {
    }

    void Update()
    {
        //rb.AddForce(0,0, forwardForce * Time.deltaTime); 
        rb.AddForce(0,0,dir * 1000 * Time.deltaTime);

    }

    public void GoRight(){
        rb.AddForce(0,0,sidewaysForce * 5 * Time.deltaTime);
    }

    public void GoLeft(){
        rb.AddForce(0,0,-sidewaysForce * 5 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.name == "LimitDir"){
            dir = -1;
        }
        if(collisionInfo.collider.name == "LimitEsq"){
            dir = 1;
        }
    }

    public float getHealth(){
        return health;
    }

    public void TakeDamage(float amount){
        health += amount;
        Debug.Log(health);
    }

}