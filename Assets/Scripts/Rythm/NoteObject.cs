using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class NoteObject : MonoBehaviour
{
    public string color;

    void Update()
    {
        if (gameObject.transform.position.y <= -2.5 && gameObject.tag == "clone")
            Destroy(gameObject);
    }

    public float getHeight()
    {
        return this.transform.position.y + 0.643f;
    }
}
