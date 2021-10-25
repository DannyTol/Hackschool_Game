using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    public float enemyHealth;
    public float bulletDamage;
    public float seePlayerDistance;
    public float timeForNextShot;
    public float timeForNextShotReload;

    public GameObject target;
    public GameObject bulletLeftPrefab;
    public Transform shootPoint;


    private void Update()
    {
        SeePlayer();

    }

    // Player Bullet gives Enemy Damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Bullet hits Enemy");
            enemyHealth -= bulletDamage;

            if(enemyHealth <= 0)
            {
                enemyHealth = 0;
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // Timer for Enemyshoot
    void Timer()
    {
        if (timeForNextShot >= 0)
        {
            timeForNextShot -= 1 * Time.deltaTime;
            if (timeForNextShot < 0)
            {
                timeForNextShot = 0;
            }
        }
    }

    // If Enemy see Player and Timer is done, Enemy will shoot
    void SeePlayer()
    {
        float distance = Vector3.Distance(FindObjectOfType<PlayerMovement>().transform.position,transform.position);

        Timer();
        
        if (distance <= seePlayerDistance && timeForNextShot == 0)
        {
            Debug.Log("See Player");
            GameObject newBullet = Instantiate(bulletLeftPrefab);
            newBullet.transform.position = shootPoint.transform.position;
            Destroy(newBullet, 1.5f);
            timeForNextShot = timeForNextShotReload;
        }
        else
        {
            Debug.Log("Can´t see Player");
        }
    }
}
