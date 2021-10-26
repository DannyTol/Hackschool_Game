using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantEnemyBulletRight : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDamage;


    private void Awake()
    {
        gameObject.tag = "EnemyBullet";
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime, 0, 0);
    }

    // Collision with Player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.tag == "Player")
            {
                FindObjectOfType<PlayerMovement>().playerHealth -= bulletDamage;
                Destroy(gameObject);
            }

            if (collision.gameObject)
            {
                Destroy(gameObject);
            }
        }

    }
}
