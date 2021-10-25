using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coinsToPlayer;
    public float rotationSpeed;


    public GameObject target;

    // coin rotates
    private void Update()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }


    // Player get coins
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target.GetComponent<PlayerMovement>().coins += coinsToPlayer;
            Debug.Log("Collision with Player");
            Destroy(gameObject);
        }
    }
}
