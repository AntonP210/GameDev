using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator animator;
    public float attackRange =0.5f;
    public int attackDamage=50;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public Transform firePoint;
    public GameObject bulletPrefab;
    Animator playerAnimator;
    public ParticleSystem particleSystem;



    private void Start()
    {
        particleSystem.GetComponent<Renderer>().sortingLayerName = "Foreground";
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) {
            Shoot();   
        }

        if (Time.time>= nextAttackTime) {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
    }
    private void Shoot() {
        particleSystem.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }
    private void Attack() {
        //Play Attack Animation
        playerAnimator.SetTrigger("attack");
        Debug.Log("Attack !!!");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        //Damage enemies
        foreach (Collider2D enemy in hitEnemies) {
            Debug.Log("HIT" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position,attackRange);
    }
}
