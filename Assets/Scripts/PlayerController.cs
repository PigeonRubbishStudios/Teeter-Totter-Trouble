using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool gameActive;
    private bool collision;
    private float accelerationSpeed;
    public float speedMultiplier;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameActive = true;
        collision = false;
    }

    // Update is called once per frame
    void Update()
    {
        accelerationSpeed = Input.acceleration.x;

        if (gameActive == true && collision == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rigidBody.AddForce(Vector2.left * 12f);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rigidBody.AddForce(Vector2.right * 12f);
            }
        }

        if (gameActive == true && collision == true)
        {
            if (Input.acceleration.x < 0)
            {
                rigidBody.AddForce(Vector2.left * (accelerationSpeed * -speedMultiplier));
            }

            if (Input.acceleration.x > 0)
            {
                rigidBody.AddForce(Vector2.right * (accelerationSpeed * speedMultiplier));
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Platform>())
        {
            collision = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PaperWeight>())
        {
            spriteRenderer.enabled = false;
            gameActive = false;
            StartCoroutine("EndGame");
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("MainMenu");
    }

    public void pauseGame()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        gameActive = false;
    }

    public void resumeGame()
    {
        rigidBody.constraints = RigidbodyConstraints2D.None;
        gameActive = true;
        rigidBody.AddForce(Vector2.down * 0.1f);
    }
}
