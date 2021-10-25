using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRight : MonoBehaviour
{
    public float enemyBulletDamage;
    public float bulletSpeed;

    private float damage;


    void Update()
    {
        transform.position += new Vector3(+bulletSpeed * Time.deltaTime, 0);
    }

    // If EnemyBullet hits Player, Player gets damage
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("EnemyBullet hits Player");
            FindObjectOfType<PlayerMovement>().playerHealth -= enemyBulletDamage;
            Destroy(gameObject);
        }

        // if Bullet hits object, Bullet destroy
        if (collision.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
