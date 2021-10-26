using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemy : MonoBehaviour
{
    public float enemyHealth;
    public float getBulletDamage;
    public int coinsToPlayer;
    public int pointsToPlayer;
    public float distanceToPlayer;
    public float startShoot;
    public float nextShoot;

    public GameObject target;
    public GameObject PlantEnemyBulletLeftPrefab;
    public GameObject PlantEnemyBulletRightPrefab;
    public Transform shootPoint;

    

    // Give PlantEnemy Tag Enemy
    private void Awake()
    {
        gameObject.tag = "Enemy";
    }

    
    private void Update()
    {
        CheckDistanceToPlayer();
    }

    
    // Collision with PlayerBullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by PlayerBullet");
            enemyHealth -= getBulletDamage;

            if (enemyHealth <= 0)
            {
                enemyHealth = 0;
                Die();
            }
        }
    }

    
    // PlantEnemy dies and give Player coins and points
    void Die()
    {
        Debug.Log("PlantEnemy is Dead");
        Destroy(gameObject);
        FindObjectOfType<PlayerMovement>().coins += coinsToPlayer;
        FindObjectOfType<PlayerMovement>().points += pointsToPlayer;
    }

    
    // PlantEnemy checks if Player is near
    void CheckDistanceToPlayer()
    {
        float distance = Vector3.Distance(FindObjectOfType<PlayerMovement>().transform.position, transform.position);

        if(distance <= distanceToPlayer)
        {
            Debug.Log("PlantEnemy can see Player");
            ShootAtPlayer();
        }
        else
        {
            Debug.Log("PlantEnemy can´t see Player");
        }
    }
    
    
    //Timeer for next Bullet
    void Timer()
    {
        if (startShoot > 0)
        {
            startShoot -= 1 *Time.deltaTime;
            
            if(startShoot < 0)
            {
                startShoot = 0;
            }
        }
    }

    // PlantEnemy starts to shoot at Player
    void ShootAtPlayer()
    {
        Timer();

        if(target.transform.position.x < transform.position.x && startShoot == 0)
        {
            GameObject newBullet = Instantiate(PlantEnemyBulletLeftPrefab);
            newBullet.transform.position = shootPoint.transform.position;
            Destroy(newBullet, 1.5f);
            startShoot = nextShoot;
        }

        if (target.transform.position.x > transform.position.x && startShoot == 0)
        {
            GameObject newBullet = Instantiate(PlantEnemyBulletRightPrefab);
            newBullet.transform.position = shootPoint.transform.position;
            Destroy(newBullet, 1.5f);
            startShoot = nextShoot;
        }
    }
}
