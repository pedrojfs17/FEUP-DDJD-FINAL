using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // private MeshRenderer currentMaterial;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material newMaterial;
    
    public NoteObject ball;

    void Start()
    {
        ball = null;
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
