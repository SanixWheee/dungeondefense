using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerCollisions : MonoBehaviour
{
    //Detects collisions between the GameObjects with colliders attached
    void OnCollisionEnter2D(Collision2D collision)
    {
        //checking for collisions with a game object
        if(collision.gameObject.tag == "Wall")
        {
            transform.position = new Vector2(4, 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started");
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
