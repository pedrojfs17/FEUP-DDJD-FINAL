using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopCollision(){
        //Destroy(gameObject);
        GetComponent<Collider>().enabled = false;
        StartCoroutine(EnableCollision());
    }

    private IEnumerator EnableCollision(){
        yield return new WaitForSeconds(2) ;
        GetComponent<Collider>().enabled = true;
    }
  
}
