using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int bulletDmg=50;
    public ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Wall")
        {
            Debug.Log(coll.name);
            particleSystem.Play();
            Destroy(gameObject);
        }
        else {
            Enemy enemy = coll.GetComponent<Enemy>();
            if (enemy != null)
            {
                //Add Colision effect
                particleSystem.Play();
                Debug.Log(coll.name);
                enemy.TakeDamage(bulletDmg);
            }
            Destroy(gameObject, 0.75f);
        }
        
        
    }
}
