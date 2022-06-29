using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 30;
    public float lifeTime = 1;
    public float distance = 0.5f;
    public float damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame 
    void Update()
    {

        transform.Translate(-transform.up * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + new Vector3(0,-1,0), transform.up,100);
    }

    void OnCollisionEnter(Collision collisionInfo){
        if(collisionInfo.collider.name == "Player1"){
            DestroyProjectile();
            collisionInfo.collider.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    void DestroyProjectile(){
        Destroy(gameObject);
    }
}
