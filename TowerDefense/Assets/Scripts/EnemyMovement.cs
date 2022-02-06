using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private Transform enemyTransform;
    private int wavePointIndex = 0;
    private float turnSpeed = 3.5f;

    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        Transform enemyTransform = enemy.GetComponentInParent(typeof(Transform)) as Transform;
        target = Waypoints.points[0];
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;

        transform.Translate(direction.normalized * enemy.speed * 0.5f * Time.deltaTime, Space.World);
        LookAtTarget();

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {

            GetNextWayPoint();
        }

    }
    void LookAtTarget()
    {
        Vector3 direction = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            EnemyReachedEndPoint();
            return;
        }
        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void EnemyReachedEndPoint()
    {
        PlayerStats.Health--;
        Destroy(gameObject);
    }
}
