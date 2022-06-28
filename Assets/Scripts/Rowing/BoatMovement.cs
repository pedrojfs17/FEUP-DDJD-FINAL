using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovement : MonoBehaviour
{
    public Vector3 leftCenter, rightCenter;
    public float leftSpeed = 0f, rightSpeed = 0f, leftAcceleration = 0f, rightAcceleration = 0f, leftPressedElapsed = 0f, rightPressedElapsed = 0f;
    
    private bool leftPressed = false, rightPressed = false;
    
    private const float ACCELERATION_STEP = 5f;
    private const float MAX_ACCELERATION = 90f;
    private const float MIN_ACCELERATION = -240f;
    private const float MAX_SPEED = 40f;
    
    // Start is called before the first frame update
    void Start()
    {
        updateAnchorPoints();
    }

    // Update is called once per frame
    void Update()
    {
        updateAnchorPoints();
        updateAccelerations(); 
        updateSpeeds(Time.deltaTime);
        updatePositions(Time.deltaTime);
    }
    
    public void pressLeft() {
        this.leftPressed = true;
    }
    
    public void raiseLeft() {
        this.leftPressed = false;
    }
    
    public void pressRight() {
        this.rightPressed = true;
    }
    
    public void raiseRight() {
        this.rightPressed = false;
    }
    
    private void updateAnchorPoints() {
        getLeftCenter();
        getRightCenter();
    }
    
    private void getLeftCenter() {
        leftCenter = transform.Find("LeftAnchor").position; // 2nd child of Boat GameObject should always be leftAnchor
    }
    
    private void getRightCenter() {
        rightCenter = transform.Find("RightAnchor").position; // 3rd child of Boat GameObject should always be rightAnchor
    }
    
    private void updateAccelerations() {
        if (leftPressed) {
            leftPressedElapsed += Time.deltaTime;
            
            // only accelerates for 1s, then player must re press the button to accelerate again
            if (leftPressedElapsed >= 1f) {
                leftDeaccelerate();
            } else {
                leftAccelerate();
            }
        } else {
            leftPressedElapsed = 0f;
            leftDeaccelerate();
        }
        
        if (rightPressed) {
            rightPressedElapsed += Time.deltaTime;
            
            // only accelerates for 1s, then player must re press the button to accelerate again
            if (rightPressedElapsed >= 1f) {
                rightDeaccelerate();
            } else {
                rightAccelerate();
            }
        } else {
            rightPressedElapsed = 0f;
            rightDeaccelerate();
        }
    }
    
    private void leftAccelerate() {
        leftAcceleration += ACCELERATION_STEP;
        if (leftAcceleration > MAX_ACCELERATION) {
            leftAcceleration = MAX_ACCELERATION;
        }
    }
    
    private void leftDeaccelerate() {
        leftAcceleration -= ACCELERATION_STEP;
        if (leftAcceleration < MIN_ACCELERATION) {
            leftAcceleration = MIN_ACCELERATION;
        }
    }
    
    private void rightAccelerate() {
        rightAcceleration += ACCELERATION_STEP;
        if (rightAcceleration > MAX_ACCELERATION) {
            rightAcceleration = MAX_ACCELERATION;
        }
    }
    
    private void rightDeaccelerate() {
        rightAcceleration -= ACCELERATION_STEP;
        if (rightAcceleration < MIN_ACCELERATION) {
            rightAcceleration = MIN_ACCELERATION;
        }
    }
    
    private void updateSpeeds(float deltaTime) {
        updateLeftSpeed(deltaTime);
        updateRightSpeed(deltaTime);
    }
    
    private void updateLeftSpeed(float deltaTime) {
        leftSpeed += leftAcceleration * deltaTime;
        if (leftSpeed > MAX_SPEED) {
            leftSpeed = MAX_SPEED;
            return;
        }
        if (leftSpeed < 0f) {
            leftSpeed = 0f;
            leftAcceleration = 0f;
        }
    }
    
    private void updateRightSpeed(float deltaTime) {
        rightSpeed += rightAcceleration * deltaTime;
        if (rightSpeed > MAX_SPEED) {
            rightSpeed = MAX_SPEED;
            return;
        }
        if (rightSpeed < 0f) {
            rightSpeed = 0f;
            rightAcceleration = 0f;
        }
    }
    
    private void updatePositions(float deltaTime) {
        transform.RotateAround(rightCenter, transform.up, leftSpeed * deltaTime);
        transform.RotateAround(leftCenter, -transform.up, rightSpeed * deltaTime);
    }
}
