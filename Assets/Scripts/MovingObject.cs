using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveDistance = 5f; 
    public float moveSpeed = 5f;
    public bool moveRight = true; 

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
        if (moveRight)
            targetPos = startPos + Vector3.right * moveDistance;
        else
            targetPos = startPos - Vector3.right * moveDistance;
    }

    void SwitchDirection()
    {
        moveRight = !moveRight;
    }
}
