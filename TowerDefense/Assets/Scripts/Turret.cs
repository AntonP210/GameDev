using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy;

    [Header("Attributes")]
    public float range = 30f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    private float fireCountDown = 0f;



    [Header("Unity Setup Fields")]
    public Transform partToRotate;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public string enemyTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }


    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }



    void Update()
    {
        if (target == null)
        {
            return;
        }


        //target lock on
        LockOnTarget();


        if (fireCountDown <= 0f)
        {
            targetEnemy = target.GetComponent<Enemy>();
            if (targetEnemy.health > 0)
            {
                Shoot();
            }


            fireCountDown = 1f / fireRate;
        }

        fireCountDown -= Time.deltaTime;


    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGo = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bullet = bulletGo.GetComponent<Bullet>();

        if (bullet != null)
        {
            if (bulletPrefab.tag == "BalistaBullet")
            {
                try
                {
                    SoundManager.instance.PlayBalistaSound();
                }
                catch (Exception e)
                {
                    Debug.Log("Play the game from main menu");
                }

            }
            if (bulletPrefab.tag == "PyroBullet")
            {

                try
                {
                    SoundManager.instance.PlayFireBallSound();
                }
                catch (Exception e)
                {
                    Debug.Log("Play the game from main menu");
                }
            }
            if (bulletPrefab.tag == "CannonBullet")
            {

                try
                {
                    SoundManager.instance.PlayCannonSound();
                }
                catch (Exception e)
                {
                    Debug.Log("Play the game from main menu");
                }
            }

            bullet.Seek(target);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
