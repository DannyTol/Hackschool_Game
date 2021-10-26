using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformLeftAndRight : MonoBehaviour
{
    public float speed;

    private bool leftFree = false;
    private bool rightFree = true;


    private void Awake()
    {
        gameObject.tag = "EnemyBullet";
    }


    private void Update()
    {
        PlattformMove();
    }

    // Plattform changes direction if collision with Wall
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall" && rightFree == true)
        {
            rightFree = false;
            leftFree = true;
        }
        else
        {
            rightFree = true;
            leftFree = false;
        }
    }

    // Plattform Move Direction by collision with PlattformStop
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "PlattformStop" && rightFree == true)
        {
            rightFree = false;
            leftFree = true;
        }
        else
        {
            rightFree = true;
            leftFree = false;
        }
    }

    // Plattform moeves in free direction (left or right)
    private void PlattformMove()
    {
        if (rightFree == true)
        {
            transform.Translate(speed * Time.deltaTime, 0,0);
            leftFree = false;
        }

        else if (leftFree == true)
        {
            transform.Translate(-speed * Time.deltaTime,0, 0);
            rightFree = false;
        }
    }
}
