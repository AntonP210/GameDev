using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public HealthPoints healthPoints;
    public UIcontroller UIcontroller;
    public ParticleSystem particleSystem;
    Animator playerAnimator;
    bool BarOrPoints; //Bar = True , Points = False

    // Start is called before the first frame update
    void Start()
    {
        BarOrPoints = UIcontroller.BarOrPoints;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerAnimator = GetComponent<Animator>();
        //Change Foreground to the layer you want it to display on 
        //You could prob. make a public variable for this
        particleSystem.GetComponent<Renderer>().sortingLayerName = "Foreground";
    }

    // Update is called once per frame
    void Update()
    {
        if (BarOrPoints)
        {
            if (Input.GetKeyDown(KeyCode.R)){
                TakeDamage(20);
                particleSystem.Play();
                Debug.Log(20 + "Damage");

            } 
            
        }
        else {
            if (Input.GetKeyDown(KeyCode.R)) {
                healthPoints.UpdateHealth(true);
                particleSystem.Play();
                Debug.Log("Damage");

            }
        }


        if (currentHealth==0) {
            playerAnimator.SetBool("death", true);
            Debug.Log("YOU DIED");
        }
        if (healthPoints.currentHealthPoints == 0) {
            playerAnimator.SetBool("death", true);
            Debug.Log("YOU DIED");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            TakeDamage(20);
            healthPoints.UpdateHealth(true);
            particleSystem.Play();
            Debug.Log(20 + "Damage");
        }
    }
    private void TakeDamage(int damage) {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    

}
