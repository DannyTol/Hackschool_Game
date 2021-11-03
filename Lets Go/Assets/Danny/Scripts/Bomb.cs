using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float giveDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Bomb collision wih Player");
            FindObjectOfType<PlayerMovement>().playerHealth -= giveDamage;
            Destroy(gameObject);
        }

        if (collision.gameObject)
        {
            //Destroy(gameObject);
        }
    }
}
