using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperWeight : MonoBehaviour
{
    public static PaperWeight instance;
    
    private Rigidbody2D rigidBody;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void pauseGame()
    {
        //rigidBody.gravityScale = 0;
        Time.timeScale = 0;
    }

    public void resumeGame()
    {
        //rigidBody.gravityScale = 1;
        Time.timeScale = 1;
    }
}
