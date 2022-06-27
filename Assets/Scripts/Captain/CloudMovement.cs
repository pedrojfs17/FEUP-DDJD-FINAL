using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private float _speed;
    private float _endPosX;

    public void StartFloating(float speed, float endPosX) 
    {
        _speed = speed;
        _endPosX = endPosX;
    }

    void Update()
    {
        transform.Translate(Vector3.left * (_speed * Time.deltaTime));

        if (transform.position.x < _endPosX) Destroy(gameObject);
    }
}
