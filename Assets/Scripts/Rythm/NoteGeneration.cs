using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGeneration : MonoBehaviour
{
    public GameObject note;
    public GameObject scroller;
    private float time;
    private bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        int rand = Random.Range(0,100);
        
        if(rand >= 10 && time >= 1f){
            GameObject clone = Instantiate(note, transform.position, Quaternion.identity, scroller.transform);
            clone.name= clone.name.Replace("(Clone)", "C");
            clone.tag = "clone";
            time=0;
        }
    }
}
