using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    public GameObject CharacterAnimator;
    [SerializeField] private float JumpForce = 400f;                          // Amount of force added when the player jumps.
         // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is grounded.


    const float GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool Grounded;            // Whether or not the player is grounded.
    private bool canDoubleJump = true; 
    private Rigidbody2D Rigidbody2D;
    private bool FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;
    
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }
    private void FixedUpdate()
    {
        bool wasGrounded = Grounded;
        Grounded = false;
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                Grounded = true;
                canDoubleJump = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }
    public void Move(float move, bool jump)
    {
        if (Grounded && Mathf.Abs(move) > 0.01)
            {
                CharacterAnimator.GetComponent<Animator>().SetBool("Running", true);
            }
        else if (!Grounded || Mathf.Abs(move) < 0.1)
            {
                CharacterAnimator.GetComponent<Animator>().SetBool("Running", false);
            } 
        //only control the player if grounded or airControl is turned on
        if (Grounded || AirControl)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }

        if (jump)
        {
            if (Grounded)
            {
                CharacterAnimator.GetComponent<Animator>().SetBool("Jumping", true);
                Grounded = false;
                Rigidbody2D.velocity = Vector2.up * JumpForce;
            }
            else
            {
                if (canDoubleJump)
                {
                    Rigidbody2D.velocity = Vector2.up * JumpForce*0.7f;
                    canDoubleJump = false;
                }
            }
        }
    }
    private void Flip()
    {
        FacingRight = !FacingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
            CharacterAnimator.GetComponent<Animator>().SetBool("Jumping", false);
    }
}
