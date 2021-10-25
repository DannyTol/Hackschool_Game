using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiv : MonoBehaviour
{
    public float enemyHealth;
    public float playerBulletDamage;
    public int pointsToPlayer;
    public int coinsToPlayer;
    public float enemySpeed;
    public float enemyHuntSpeed;
    public float targetDistance;

    public GameObject target;

    private Rigidbody2D rb;

    private bool wall;
    private bool freeWayRight;
    private bool freeWayLeft = true;
    private bool seePlayer = false;
    
    void Update()
    {
        
        GoToPlayer();
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision with Player´s Bullet
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Collision with Bullet");
            enemyHealth -= playerBulletDamage;
            Destroy(collision.gameObject);

            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                Die();
            }
        }


        // Collision with Player
        if (collision.gameObject.tag == "Player")
        {
            Die();
        }

        
        if (collision.gameObject)
        {
            //if (seePlayer == false)
            //{
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
            //}

            if (collision.gameObject.tag == "Wall" && seePlayer == true)
            {
                    Destroy(collision.gameObject);
            }

            
        }
    }

    
    //EnemyExplosiv Death
    void Die()
    {
        target.GetComponent<PlayerMovement>().points += pointsToPlayer;
        target.GetComponent<PlayerMovement>().coins += coinsToPlayer;
        Debug.Log("EnemyExplosiv died");
        Destroy(gameObject);
    }


    // EnemyExplosiv moves to Player if distance is right
    void GoToPlayer()
    {
        float distance = Vector3.Distance(FindObjectOfType<PlayerMovement>().transform.position, transform.position);
        
        if (distance <= targetDistance)
        {
            seePlayer = true;                
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemyHuntSpeed * Time.deltaTime);
        }
        else if (distance > targetDistance)
        {
            //seePlayer = false;
            EnemyMove();
        }
       
    }

    // Moves in free direction (left or right)
    void EnemyMove()
    {

        if (freeWayRight == true)
        {
            transform.Translate(+enemySpeed * Time.deltaTime, 0, 0);
            freeWayLeft = false;
        }
        else if (freeWayLeft == true)
        {
            transform.Translate(-enemySpeed * Time.deltaTime, 0, 0);
            freeWayRight = false;
        }
    }
}
