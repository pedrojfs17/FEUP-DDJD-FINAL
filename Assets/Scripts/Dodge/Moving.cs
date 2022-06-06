using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        transform.position += (velocity * Time.deltaTime); 
    }


    void OnCollisionEnter2D(Collision2D collision){
        collision.collider.transform.SetParent(transform);
    }
}
