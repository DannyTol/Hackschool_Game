using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformUpAndDown : MonoBehaviour
{
    public float speed;

    private bool upFree = true;
    private bool downFree = false;
    
    private void Update()
    {
        PlattformMove();
    }

    // Plattform changes direction if collision with Wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && upFree == true)
        {
            upFree = false;
            downFree = true;
        }
        else
        {
            upFree = true;
            downFree = false;
        }
    }

    // Plattform Move Direction by collision with PlattformStop
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlattformStop" && upFree == true)
        {
            upFree = false;
            downFree = true;
        }
        else
        {
            upFree = true;
            downFree = false;
        }
    }

    // Plattform moves in free direction(up or down)
    private void PlattformMove()
    {
        if (upFree == true)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            downFree = false;
        }

        else if(downFree == true)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            upFree = false;
        }
    }
}
