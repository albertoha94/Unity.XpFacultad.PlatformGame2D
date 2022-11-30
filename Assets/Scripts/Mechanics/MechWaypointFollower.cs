using UnityEngine;
using UnityEngine.Events;

public class MechWaypointFollower : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float distanceThreshold = .1f;
    [SerializeField] private float waitTimeBetweenPoints = 2f;
    [SerializeField] private bool isMovingRight = true;

    [Header("Events")]
    [SerializeField] private UnityEvent onMovementStopped;
    [SerializeField] private UnityEvent onMovementStartLeft, onMovementStartRight;

    private int currentWaypointIndex = 0;
    private bool canMove = true;
    private float canMoveTimer = 0;
    private float previousXPosition = 0;

    public bool IsMovingRight
    {
        get { return isMovingRight; }
        set
        {
            if (isMovingRight != value)
            {
                if (value)
                {
                    onMovementStartRight.Invoke();
                }
                else
                {
                    onMovementStartLeft.Invoke();
                }
            }

            isMovingRight = value;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var deltaTime = Time.deltaTime;
        if (canMove)
        {

            var currentPosition = transform.position;
            var currentWaypointPosition = waypoints[currentWaypointIndex].position;
            if (Vector2.Distance(currentWaypointPosition, currentPosition) < distanceThreshold)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }

                if (canMove && waitTimeBetweenPoints > 0)
                {
                    onMovementStopped.Invoke();
                    canMove = false;
                }
            }
            else
            {
                var newPosition = Vector2.MoveTowards(currentPosition, currentWaypointPosition, speed * deltaTime);
                var newXPosition = newPosition.x;
                transform.position = newPosition;
                if (newXPosition != previousXPosition)
                {
                    IsMovingRight = newXPosition > previousXPosition ? true : false;
                    previousXPosition = newXPosition;
                }
            }
        }
        else
        {
            if (canMoveTimer >= waitTimeBetweenPoints)
            {
                canMove = true;
                canMoveTimer = 0;
            }
            else
            {
                canMoveTimer += deltaTime;
            }
        }
    }
}
