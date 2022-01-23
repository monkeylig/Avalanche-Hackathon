using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    public enum MovementDirection { Left = 1, Right = 2, Down = 3, Up = 4, None = 0 }

    [SerializeField]
    private float groundMovementSpeed = 0;
    [SerializeField]
    private float arialMovementSpeed = 0;
    [SerializeField]
    private float maxSpeed = 0;
    [SerializeField]
    private float jumpForce = 0;
    [SerializeField]
    private float groundDrag = 0;
    [SerializeField]
    private float arialDrag = 0;

    private Animator animator;
    private Rigidbody2D rb2d;
    private Collider2D colliderTrigger = null;
    private Collider2D[] possibleGroundTriggers = null;
    private Transform focusPoint = null;
    private MovementDirection horizontalMovement;
    private MovementDirection faceDirection;
    private bool isOnGround = true;
    private bool isJumping = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Collider2D[] colliders;
        colliders = GetComponents<BoxCollider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.isTrigger)
            {
                colliderTrigger = collider;
            }
        }
        //focusPoint = transform.GetChild(0);

        if (!focusPoint)
        {
            focusPoint = transform;
        }

        horizontalMovement = MovementDirection.None;
        faceDirection = MovementDirection.Right;

        possibleGroundTriggers = new Collider2D[2];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed), rb2d.velocity.y);
    }

    private void FixedUpdate()
    {
        float movementSpeed;
        if (isOnGround)
        {
            movementSpeed = groundMovementSpeed;
        }
        else
        {
            movementSpeed = arialMovementSpeed;
        }

        if (horizontalMovement != MovementDirection.None && isOnGround)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if (horizontalMovement == MovementDirection.Right)
        {
            if (faceDirection == MovementDirection.Left)
            {
                rb2d.transform.Rotate(0f, 180f, 0f);
            }
            faceDirection = MovementDirection.Right;
            rb2d.AddForce(new Vector2(movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }
        else if (horizontalMovement == MovementDirection.Left)
        {
            if (faceDirection == MovementDirection.Right)
            {
                rb2d.transform.Rotate(0f, 180f, 0f);
            }
            faceDirection = MovementDirection.Left;
            rb2d.AddForce(new Vector2(-movementSpeed * Time.deltaTime, 0), ForceMode2D.Impulse);
        }

        if (isOnGround && isJumping)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            isJumping = false;
        }

        if (isOnGround)
        {
            rb2d.drag = groundDrag;
        }
        else
        {
            rb2d.drag = arialDrag;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            for(int i = 0; i < possibleGroundTriggers.Length; i++)
            {
                possibleGroundTriggers[i] = null;
            }

            ContactFilter2D filter = new ContactFilter2D();
            filter.SetLayerMask(LayerMask.GetMask("Ground"));
            colliderTrigger.OverlapCollider(filter, possibleGroundTriggers);

            //If we got any result then we are in the ground
            isOnGround = possibleGroundTriggers[0];
        }
    }

    public void Run(MovementDirection direction)
    {
        horizontalMovement = direction;
    }

    public void Jump()
    {
        if (isOnGround)
        {
            isJumping = true;
        }
    }
}
