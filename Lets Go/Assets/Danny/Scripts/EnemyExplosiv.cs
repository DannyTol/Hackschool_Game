using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosiv : MonoBehaviour
{
    public float enemyHealth;
    public float playerBulletDamage;
    
    [Space]
    public int pointsToPlayer;
    public int coinsToPlayer;
    
    [Space]
    public float enemySpeed;
    public float enemyHuntSpeed;
    public float targetDistance;

    [Space]
    public GameObject target;
    [Space]
    public GameObject effectPrefab;
  
    private bool freeWayRight;
    private bool freeWayLeft = true;
    private bool seePlayer = false;
    private bool canNotSeePlayer;
    
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

        
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "BigWall")
        {
            //if (seePlayer == false)
            //{
                if (collision.gameObject.tag == "Wall" && freeWayLeft == true || collision.gameObject.tag == "BigWall" && freeWayLeft == true)
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
        GameObject newEffect = Instantiate(effectPrefab);
        newEffect.transform.position = transform.position;
        Destroy(newEffect,0.25f);
    }


    // EnemyExplosiv moves to Player if distance is right
    void GoToPlayer()
    {
        GameObject BigWall = GameObject.FindGameObjectWithTag("BigWall");
        float distance = Vector3.Distance(FindObjectOfType<PlayerMovement>().transform.position, transform.position);
        float distanceWall = Vector3.Distance(BigWall.transform.position, transform.position);

        // Enemy checks if there is a Wall between him and Player
        if(distanceWall < distance)
        {
            canNotSeePlayer = true;
        }
        else
        {
            canNotSeePlayer = false;
        }

        if (distance <= targetDistance && canNotSeePlayer == false)
        {
            seePlayer = true;                
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemyHuntSpeed * Time.deltaTime);
        }
        else if (distance > targetDistance || canNotSeePlayer == true)
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
