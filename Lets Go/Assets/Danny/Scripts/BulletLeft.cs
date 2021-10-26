using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLeft : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject effect;
    public GameObject Bullethole;
    void Update()
    {
        transform.position -= new Vector3(bulletSpeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyExplosiv")
        {
            Debug.Log("Bullet hits Enemy");
        }

        // Bullet Destroy if collision with any Object
        if (collision.transform)
        {
            Die();
        }

        // Bullet creates Effect and Bullethole when Bullet destroy
        void Die()
        {
            if (collision.gameObject.tag == "EnemyExplosiv" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
            {
                Destroy(gameObject);
                GameObject newEffect = Instantiate(effect);
                newEffect.transform.position = gameObject.transform.position;
                Destroy(newEffect, 0.15f);
            }
            else
            {
                Destroy(gameObject);
                GameObject newEffect = Instantiate(effect);
                newEffect.transform.position = gameObject.transform.position;
                Destroy(newEffect, 0.15f);
                GameObject newHole = Instantiate(Bullethole);
                newHole.transform.position = collision.transform.position;
                Destroy(newHole, 1);
            }
        }
    }
}
