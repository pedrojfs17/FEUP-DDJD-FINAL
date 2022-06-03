using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // private MeshRenderer currentMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material newMaterial;
    
    public NoteObject ball;

    // Start is called before the first frame update
    void Start()
    {
        ball = null;
        // currentMaterial = GetComponent<MeshRenderer>();
    }

    public NoteObject hitBall()
    {
        NoteObject currentBall = ball;
        ball = null;
        return currentBall;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag=="clone")
            ball=other.GetComponent<NoteObject>();
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag=="clone")
            ball = null;
    }
}
