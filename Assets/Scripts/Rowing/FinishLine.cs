using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boat") {
            GameObject.Find("GameLogic").GetComponent<RowingLogic>().finish(other.name);
        }
    }
}
