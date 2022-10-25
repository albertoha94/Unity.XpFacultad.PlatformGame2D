using UnityEngine;

public class MechWaypointFollower : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;

    private int currentWaypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].position, transform.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
    }
}
