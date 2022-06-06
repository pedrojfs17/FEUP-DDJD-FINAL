using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject projectile;
    public Transform shotPoint;
    public string key;

    public float timeBtwShots;
    public float startTimeShots = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwShots > 0){
            timeBtwShots -= Time.deltaTime;
        }

        if(Input.GetKey(key)){
            Shoot();
        }
    }

    bool canShoot(){
        if(timeBtwShots <= 0){
            return true;
        }
        else{
            return false;
        }
    }

    void ChangeTime(){
        timeBtwShots = startTimeShots;
    }

    public void Shoot()
    {
        if(canShoot() == true){
            Instantiate(projectile, shotPoint.position, transform.rotation);
            ChangeTime();
        }
    }
}
