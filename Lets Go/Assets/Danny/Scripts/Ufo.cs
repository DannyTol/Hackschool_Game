using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    [Space]
    public float health;
    public float playerBulletDamage;
    [Space]
    public float speed;
    [Space]
    public Transform shootPoint1;
    public Transform shootPoint2;
    public Transform shootPoint3;
    [Space]
    public GameObject bombPrefab;
    [Space]
    public float shootTime = 0;
    public float nextShoot;


    private bool rightFree = true;
    private Transform shootPoint;
    
    private void Update()
    {
        Move();

        Shoot();
    }

    private void Move()
    {
        if (rightFree == true)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            
        }
        else if (rightFree == false)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            //rightFree = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlattformStop" && rightFree == true)
        {
            Debug.Log("Ufo Collision with PlattformStop");
            rightFree = false;
        }
        else if(collision.gameObject.tag == "PlattformStop" && rightFree == false)
        {
            rightFree = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Debug.Log("Ufo collision with PlayerBullet");
            health -= playerBulletDamage;
            
            if(health <= 0)
            {
                health = 0;
                Die();
            }
        }
    }

    void Timer()
    {
        nextShoot = Random.Range(0.5f, 1.2f);
        
        if (shootTime > 0)
        {
            shootTime -= 1 *Time.deltaTime;
            if(shootTime <= 0)
            {
                shootTime = 0;
            }
        }
        else
        {
            shootTime = nextShoot;
        }
    }

    void Shoot()
    {
        Timer();


        float point = 0;
        point = Random.Range(1,4);

        if (shootTime == 0)
        {
            switch (point)
            {
                case 1:
                    GameObject newBomb = Instantiate(bombPrefab);
                    newBomb.transform.position = shootPoint1.transform.position;
                    Destroy(newBomb, 1.2f);
                    break;
                case 2:
                    GameObject newBomb1 = Instantiate(bombPrefab);
                    newBomb1.transform.position = shootPoint2.transform.position;
                    Destroy(newBomb1, 1.2f);
                    break;
                case 3:
                    GameObject newBomb2 = Instantiate(bombPrefab);
                    newBomb2.transform.position = shootPoint3.transform.position;
                    Destroy(newBomb2, 1.2f);
                    break;
            }
        }

    }

    void Die()
    {
        Debug.Log("Ufo died");
        Destroy(gameObject);
    }
}
