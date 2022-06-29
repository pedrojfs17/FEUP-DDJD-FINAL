using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGeneration : MonoBehaviour
{
    public List<GameObject> notes;
    public GameObject scroller;
    private float time;
    private bool hasStarted;
    public RythmLogic rythmLogic;
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!rythmLogic.playing) return;

        time += Time.deltaTime;

        if (time < 1.0f) return;
        
        int noteIndex = Random.Range(0, notes.Count);

        GameObject clone = Instantiate(notes[noteIndex], transform.position, Quaternion.identity, scroller.transform);
        clone.name= clone.name.Replace("(Clone)", "C");
        clone.tag = "clone";
        time=0;
    }
}
