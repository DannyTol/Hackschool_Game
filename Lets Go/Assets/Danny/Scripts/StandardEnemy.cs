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
    public GameObject bulletRightPrefab;
    public Transform shootPoint;

    private bool freeWayRight;
    private bool freeWayLeft = true;

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

        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "BigWall")
        {
            Debug.Log("Enemy collision with Wall");
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
            if (transform.position.x > target.transform.position.x)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);

                GameObject newBullet = Instantiate(bulletLeftPrefab);
                newBullet.transform.position = shootPoint.transform.position;
                Destroy(newBullet, 1.5f);
                timeForNextShot = timeForNextShotReload;
            }
            if(transform.position.x < target.transform.position.x)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back);

                GameObject newBullet = Instantiate(bulletRightPrefab);
                newBullet.transform.position = shootPoint.transform.position;
                Destroy(newBullet, 1.5f);
                timeForNextShot = timeForNextShotReload;
            }
        }
        if (distance >= seePlayerDistance)
        {
            Debug.Log("Can�t see Player");
            Move();
        }
    }

    // Enemy moves
    void Move()
    {
        if (freeWayRight == true)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            freeWayLeft = false;
        }
        else if (freeWayLeft == true)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            freeWayRight = false;
        }
    }
}
