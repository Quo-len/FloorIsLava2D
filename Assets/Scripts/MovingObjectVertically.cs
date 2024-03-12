using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectVertically : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 5f;
    public bool moveUpwards = true;

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        CalculateTargetPosition();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (transform.position == targetPos)
        {
            SwitchDirection();
            CalculateTargetPosition();
        }
    }

    void CalculateTargetPosition()
    {
        if (moveUpwards)
            targetPos = startPos + Vector3.up * moveDistance;
        else
            targetPos = startPos - Vector3.up * moveDistance;
    }

    void SwitchDirection()
    {
        moveUpwards = !moveUpwards;
    }
}
