using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ParticleSystem particleSystem;
    Animator enemyAnimator;
    public int maxHealth = 100;
    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        particleSystem.GetComponent<Renderer>().sortingLayerName = "Foreground";
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage) { 

        currentHealth-=damage;
        particleSystem.Play();
        //hurt animation
        if (currentHealth<=0) {
            Die();
        }

    }
    void Die() {
        Debug.Log("DIED");
        enemyAnimator.SetBool("death", true);
        Destroy(gameObject, 0.7f);
        //Disable the enemy
    }
    
}
