using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour
{
    public float enemyHealth;
    public float bulletDamage;
    public float speed;
    public float seePlayerDistance;
    public float timeForNextShot;
    public float timeForNextShotReload;

    public GameObject target;
    public GameObject bulletLeftPrefab;
    public Transform shootPoint;

    private bool freeWayRight;
    private bool freeWayLeft = true;

    private void Update()
    {
        SeePlayer();
        Move();

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {

            if (collision.gameObject && freeWayLeft == true)
            {
                freeWayLeft = false;
                freeWayRight = true;
            }
            else
            {
                freeWayRight = false;
                freeWayLeft = true;
            }
        }
    }

    void Move()
    {
        if (freeWayRight == true)
        {
            transform.Translate(+speed * Time.deltaTime, 0, 0);
            freeWayLeft = false;
        }
        else if (freeWayLeft == true)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            freeWayRight = false;
        }
    }
}
