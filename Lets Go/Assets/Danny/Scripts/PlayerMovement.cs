using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Space]
    public float playerHealth;
    public float speed;
    public float jumpVelocity;
    
    [Space]
    public int coins;
    public int points;
    public int mediKit;

    [Space]
    public bool isGrounded;

    public Rigidbody2D rb;
    public Animator anim;
    
    [Space]
    public GameObject bulletLeftPrefab;
    public GameObject bulletRightPrefab;
    public Transform shootPoint;

    [Space]
    public Text coinText;
    public Text mediKitText;

    [Space]
    public GameObject playerDamageSpritePrefab;

    private bool dirForward = true;
    private bool lookToForward;
    private float moveSpeed;

    private void Start()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isJumping", false);
    }

    private void Update()
    {
        Move();

        RotationAndShoot();

        Jump();

        MediKit();

        Collect();

       
    }

    // PlayerMovement
    private void Move()
    {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.A))
        {
            moveSpeed = speed;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            anim.SetBool("isWalkingBack", true);

            if (Input.GetKeyUp(KeyCode.A))
            {
                anim.SetBool("isWalkingBack", false);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveSpeed = speed;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            dirForward = false;
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.D) && dirForward == true)
        {
            moveSpeed = speed;
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            anim.SetBool("isWalking", true);

        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", false);
        }


        if (Input.GetMouseButton(1)&& Input.GetKey(KeyCode.D))
        {
            moveSpeed = speed;
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            anim.SetBool("isWalkingBack", true);

            if (Input.GetKeyUp(KeyCode.D))
            {
                anim.SetBool("isWalkingBack", false);
            }
        }  
        else if (Input.GetKey(KeyCode.D))
        {    
            moveSpeed = speed;
            rb.velocity = new Vector2(+moveSpeed, rb.velocity.y);
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            dirForward = true;
            anim.SetBool("isWalking", true);  
        }
        else if(Input.GetMouseButton(1)&&Input.GetKey(KeyCode.A)&& dirForward == false)
        {
            moveSpeed = speed;
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            anim.SetBool("isWalking", true);

        }
    }

    // Player Shoot 
    private void RotationAndShoot()
    {
        if (Input.GetMouseButtonDown(0) && dirForward == false)
        {
            Debug.Log("Shot left");
            GameObject newLeftBullet = Instantiate(bulletLeftPrefab);
            newLeftBullet.transform.position = shootPoint.transform.position;
            Destroy(newLeftBullet, 0.75f);
        }

        if (Input.GetMouseButtonDown(0) && dirForward == true)
        {
            Debug.Log("Shot right");
            GameObject newRightBullet = Instantiate(bulletRightPrefab);
            newRightBullet.transform.position = shootPoint.transform.position;
            Destroy(newRightBullet, 0.75f);
        }
    }

    // Player jumps
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true || Input.GetKey(KeyCode.Joystick1Button1) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpVelocity;
            isGrounded = false;
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalkingBack", false);
            anim.SetBool("isJumping", true);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }
    }

    // Get a message if player colldide with Objects and set bool isGrounded true
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collision with" + collision.transform.name);
        isGrounded = true;

        // Player gets damage if collides with EnemyExplosiv
        if (collision.gameObject.tag == "EnemyExplosiv")
        {
            Debug.Log("Collision with EnemyExplosiv");
            playerHealth -= 25;
            GameObject playerDamageSprite = Instantiate(playerDamageSpritePrefab);
            playerDamageSprite.transform.position = gameObject.transform.position;
            Destroy(playerDamageSprite, 0.1f);


            PlayerHealth();
        }

        if(collision.gameObject.tag == "EnemyBullet")
        {
            GameObject playerDamageSprite = Instantiate(playerDamageSpritePrefab);
            playerDamageSprite.transform.position = gameObject.transform.position;
            Destroy(playerDamageSprite, 0.1f);
        }
        
    }

    // Player stays on Plattform (Player moves with Plattform)
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plattform")
        {
            transform.parent = collision.transform;
            Debug.Log("Collision with Plattform");
        }
    }

    // Player leaves Plattform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Plattform")
        {
            transform.parent = null;
            Debug.Log("Collision Exit Plattform");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player collect Medikit
        if (collision.gameObject.tag == "Medikit" && mediKit < 10)
        {
            Debug.Log("U got a Medikit");
            Destroy(collision.gameObject);
            mediKit++;
        }
    }

    // Player use Medikit 
    void MediKit()
    {
        if(Input.GetKeyDown(KeyCode.Space) && mediKit >= 1 && playerHealth < 100)
        {
            Debug.Log("Player use Medikit");
            mediKit -= 1; 
            playerHealth += 25;
            
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
        }
    }

    void Collect()
    {
        coinText.text = coins.ToString();
        mediKitText.text = mediKit.ToString();
    }

    void PlayerHealth()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player is dead");
    }
}
