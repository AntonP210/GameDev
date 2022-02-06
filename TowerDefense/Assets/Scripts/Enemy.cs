using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    public float startSpeed = 10f;
    public float startHealth = 100;
    [HideInInspector]
    public float health;
    public int MoneyReward = 5;
    float time;
    [Header("Unity Stuff")]
    public Image healthBar;
    Animator m_Animator;
    private EnemyMovement movementComponent;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        movementComponent = gameObject.GetComponent<EnemyMovement>();
        m_Animator = gameObject.GetComponent<Animator>();
        InvokeRepeating("stopSlow", 5f, 5f);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Slow(float amount)
    {
        m_Animator.SetBool("Slow", true);
        speed = startSpeed * (1f - amount);
    }

    void stopSlow()
    {
        m_Animator.SetBool("Slow", false);
        speed = startSpeed;
    }
    void Die()
    {
        try
        {
            healthBar.transform.parent.gameObject.transform.position = new Vector3(0f, 0f, 0f);
        }
        catch
        {
            Debug.Log("obj already distroyed");
        }

        Destroy(healthBar);

        movementComponent.enabled = false;

        m_Animator.SetTrigger("Die");

        PlayerStats.Money += MoneyReward;

        Destroy(gameObject, 2f);

    }


}
