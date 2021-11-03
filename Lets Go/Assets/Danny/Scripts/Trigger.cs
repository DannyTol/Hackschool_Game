using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public GameObject target;
    
    
    
    // If collision with Player, Trigger destroys object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Contact to Player");
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }
}
