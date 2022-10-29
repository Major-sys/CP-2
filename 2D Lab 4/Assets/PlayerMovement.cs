using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float movementRyvok = 50f;
    public float jumpHeight = 5f;
    BoxCollider2D boxCollider;
    Vector2 movementVector;
    Rigidbody2D rbody;
    private SpriteRenderer sprite;
    private Animator anim;
    
    // Start is called before the first frame update

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) State = States.idle;

        if (Input.GetButton("Horizontal"))
        { State = States.run; }


        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        { State = States.jump; }


        Vector2 playerVelocity = new Vector2(movementVector.x * movementSpeed, rbody.velocity.y);
        rbody.velocity = playerVelocity;

        
    }

    private void OnMove(InputValue value)
    {
        if (boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) State = States.run;

        movementVector = value.Get<Vector2>();
        Debug.Log(movementVector);

        sprite.flipX = movementVector.x < 0.0f;
    }



    private void OnJump(InputValue value)
    {
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;
        if (value.isPressed)
        {
            
            rbody.velocity += new Vector2(0f, jumpHeight);
        }
        if (!boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) State = States.jump;
    }

    public enum States
    { 
        idle,
        run,
        jump
    }

}
