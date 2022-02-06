using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [Header("Global Settings")]
    private Transform target;

    public GameObject impactEffect;
    public GameObject dotEffect;
    public float speed = 70f;
    public float delay = 1f;
    public int damage = 50;
    public float explosionRadius = 0f;

    [Header("Slow Settings")]
    public bool IsSlow = false;
    public float slowAmount = .3f;

    [Header("Damage Over Time Settings")]
    public bool isDot = false;
    public float damageOverTimeAmount = 150f;
    public float damageOverTimeduration = 2f;


    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }


    public void HitTarget()
    {
        GameObject effectInstance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);

        if (isDot)
        {
            TriggerDOT();
        }

        Destroy(effectInstance, 1f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }


        Destroy(gameObject);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    void Damage(Transform enemyGo)
    {
        Enemy EnemyScript = enemyGo.GetComponent<Enemy>();

        if (EnemyScript != null)
        {
            EnemyScript.TakeDamage(damage);
        }

    }

    void CreateFireEffectOnTarget()
    {

        GameObject dotEffectInstance = (GameObject)Instantiate(dotEffect, transform.position, transform.rotation);
        dotEffectInstance.transform.parent = target.transform;
        if (dotEffect != null)
        {
            Slow();
            StartCoroutine(DamageOverTime(damageOverTimeAmount, damageOverTimeduration));
        }

        Destroy(dotEffectInstance, 5f);

    }

    IEnumerator DamageOverTime(float damageAmount, float duration)
    {
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / duration;

        while (amountDamaged < damageAmount)
        {
            target.gameObject.GetComponent<Enemy>().TakeDamage(damagePerLoop);
            //Debug.Log("health: " + target.gameObject.GetComponent<Enemy>().health);
            amountDamaged += damagePerLoop;

            yield return new WaitForSeconds(1f);
        }

    }
    void Slow()
    {
        target.GetComponent<Enemy>().Slow(slowAmount);
    }
    void TriggerDOT()
    {
        //Debug.Log("trigger burning");
        CreateFireEffectOnTarget();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
