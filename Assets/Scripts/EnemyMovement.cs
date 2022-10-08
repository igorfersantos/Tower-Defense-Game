using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, enemy.speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex == Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        target = Waypoints.waypoints[++waypointIndex];
    }

    private void EndPath()
    {
        GameManager.Instance.ReducePlayerLives();
        Destroy(gameObject);
    }
}
