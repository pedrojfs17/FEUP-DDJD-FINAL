using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    [SerializeField] Transform targetArea;
    private Vector3 targetPosition;

    private float speed = 3;

    private bool moving = false;

    void Start()
    {
        targetPosition = targetArea.position + new Vector3(
            Random.Range(-targetArea.localScale.x / 2.0f, targetArea.localScale.x / 2.0f),
            -0.5f,
            Random.Range(-targetArea.localScale.z / 2.0f, targetArea.localScale.z / 2.0f)
        );

        StartCoroutine(StartMoving());
    }

    IEnumerator StartMoving()
    {
        yield return new WaitForSeconds(4.0f);

        moving = true;
    }

    void Update()
    {
        if (!moving) return;

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
        {
           moving = false;
           transform.LookAt(new Vector3(0, -2f, -10));
        }
    }
}
