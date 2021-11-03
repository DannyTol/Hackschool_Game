using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSprite : MonoBehaviour
{
    public GameObject target;

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent = collision.transform;
            transform.parent.rotation = transform.rotation = FindObjectOfType<PlayerMovement>().transform.rotation;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.parent = collision.transform;
            transform.parent.rotation = transform.rotation = FindObjectOfType<PlayerMovement>().transform.rotation;
        }
    }
}
