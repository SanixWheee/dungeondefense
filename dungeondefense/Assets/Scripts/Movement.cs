using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //variables wowie
    public float speed;
    private float topSpeed;
    public float jumpingPower;
    float movement;
    float acceleration;
    float finalSpeed;
    float decelerate;
    float stoppingPoint;
    float currentSlow;
    float deceleration;

    public float cooldown;
    public float lastJump;
    public float time;

    private bool hasJumped;

    //SerializeField makes it visible in the unity editor, but its still considered a private variable
    [SerializeField] private Rigidbody2D rb; //RigidBody2d gives the object physics controls like velocity and gravity
    [SerializeField] private Transform groundCheck; //transform is the xyz position of the object
    [SerializeField] private LayerMask groundLayer; //something that you can add different layers to and only objects in those layers will be effected

    void Start() //called once the game starts
    {
        //initializing some variables + components needed
        rb = GetComponent<Rigidbody2D>();
        topSpeed = 7f;
        acceleration = 0.5f;
        decelerate = -0.6f;
        stoppingPoint = 0f;
        speed = 10f;
        jumpingPower = 10f;
    }


    void Update() //updates the game every frame
    {
        time = Time.time;

        //Allows you to move right
        if (Input.GetKey(KeyCode.D))
        {
            topSpeed = 7f; //these three lines gradually increases the players movement speed up to the topSpeed (acceleration), making for a smoother start when moving
            finalSpeed = topSpeed - rb.velocity.x; 
            movement = finalSpeed * acceleration;
            if (rb.velocity.x < 7)
            {
                rb.AddForce(transform.right * movement); //AddForce allows you to add a 1 time instantatneous force onto any object, and we use this to add a force to the player so that it moves. We multiply it by 'movement' because that is what determines how fast the player should be moving at any time
            }
        }
        else
        {
            if (rb.velocity.x != 0) //this code allows for a bit of sliding when you stop moving (decelleration), making it feel smoother, and not like a sudden stop. Its the same as the acceleration, but in reverse
            {
                if (rb.velocity.x > 0)
                {

                    currentSlow = stoppingPoint + rb.velocity.x;
                    deceleration = currentSlow * decelerate;
                    rb.AddForce(transform.right * deceleration);

                }
            }
        }

        //Allows you to move left
        if (Input.GetKey(KeyCode.A))
        {
            topSpeed = -7f; //same acceleration as above, execept we use a negative number to move left
            finalSpeed = topSpeed - rb.velocity.x;
            movement = finalSpeed * acceleration;
            if (rb.velocity.x > -7)
            {
                rb.AddForce(transform.right * movement); //AddForce only has three directions by default, 'forward', which adds a force based on the rotation of the object, 'up' which adds the force upwards, and 'right' which adds it to the right of the object. We can invert these by using negative numbers
            }

        }
        else
        {
            if (rb.velocity.x != 0) //same decelleration as above
            {
                if (rb.velocity.x < 0)
                {

                    currentSlow = stoppingPoint + rb.velocity.x;
                    deceleration = currentSlow * decelerate;
                    rb.AddForce(transform.right * deceleration);

                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true) //This checks for if you are touching the ground AND you press space bar
        {
            hasJumped = true;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); //Physics2D.OverlapCircle creates an invisible circle around a transform of the gameObject that you tell it to, and if that circle is overlapping with a thing in the 'groundLayer' (the ground) it returns true.
    }

    private void FixedUpdate() //only runs on rendered frames
    {
        if (hasJumped == true) //jumping code
        {
            rb.gravityScale = 2f; //changes the gravity scale of the rigidbody attached to the gameobject, making jumping upwards feel more impactful
            rb.AddForce(transform.up * jumpingPower, ForceMode2D.Impulse); //adds a force upwards
            Debug.Log(rb.velocity.y); //how you print to the console in unity
            hasJumped = false;

        }
        else if (rb.velocity.y < 0.15f && rb.velocity.y > -0.15f) //during the very peak of your jump, this allows you to gain a little bit of extra height, as it feels more natural or something idk why, but it just feels better
        {
            rb.gravityScale = 0.5f;
        }
        else if (rb.velocity.y < -0.15f) //increases gravity if you are falling, to make it feel more snappy
        {
            rb.gravityScale = 3;
        }
    }
}

