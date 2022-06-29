using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool time = true;
    void Start()
    {
        StartCoroutine(waitTime());
    }

    // Update is called once per frame
    void Update()
    {
        if(time == true){
            StartCoroutine(waitTime());
            //waitTime();
        }
    }

    IEnumerator waitTime(){
        time = false;

        Debug.Log("5 Seconds Left");
        yield return new WaitForSeconds(1);
        Debug.Log("4 Seconds Left");
        yield return new WaitForSeconds(1);
        Debug.Log("3 Seconds Left");
        yield return new WaitForSeconds(1);
        Debug.Log("2 Seconds Left");
        yield return new WaitForSeconds(1);
        Debug.Log("1 Seconds Left");
        yield return new WaitForSeconds(1);
        Debug.Log("Falling!");

        float randomNumber = Random.Range(1,5);

        if(randomNumber == 1){
            GameObject.Find("Ground1").GetComponent<Ground>().StopCollision();
        }
        else if (randomNumber == 2){
            GameObject.Find("Ground2").GetComponent<Ground>().StopCollision();
        }
        else if(randomNumber == 3){
            GameObject.Find("Ground3").GetComponent<Ground>().StopCollision();
        }
        else if(randomNumber == 4){
            GameObject.Find("Ground4").GetComponent<Ground>().StopCollision();
        }

        time = true;

        GameObject.Find("GameLogic").GetComponent<FallLogic>().IncreaseHealth();
    }
}
