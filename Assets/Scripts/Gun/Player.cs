using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool isMoving = false;
    private int interpolationFramesCount = 120;
    private int elapsedFrames = 0; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Stop player from moving when
        if(elapsedFrames > interpolationFramesCount)
        {  
            Debug.Log("STOPED");
            elapsedFrames = 0;
            isMoving = false;
        }

        if(isMoving)
        {
            elapsedFrames++;
        }
    }

    void FixedUpdate() {
        if(isMoving){
            
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;

            transform.position = Vector3.Lerp(startPosition, endPosition, interpolationRatio);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, interpolationRatio);
        }
    }

    public void GoToPosition(Vector3 position, Quaternion rotation)
    {
        if(position == transform.position)
        {
            return;
        }
        
        startPosition = transform.position;
        endPosition = position;
        startRotation = transform.rotation;
        endRotation = rotation;

        isMoving = true;
    }
}
