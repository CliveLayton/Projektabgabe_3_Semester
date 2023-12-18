using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMover : MoverBase
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Animator animator;

    private Rigidbody2D rigidBody;

    //set rigidbody to the attached rb component
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// adding Force to the rb to move the object, multiply the Vector2 with the movementSpeed
    /// setting the animator variables of moveDirections to the numbers from the Vector2 
    /// </summary>
    /// <param name="direction">Vector2</param>
    public void MoveInDirection(Vector2 direction)
    {
        rigidBody.AddForce(direction * movementSpeed, ForceMode2D.Force);
        if (direction.magnitude > 0)
        {
            animator.SetFloat("moveDirectionX", direction.x);
            animator.SetFloat("moveDirectionY", direction.y);
        }
    }

    //sets the animator variable of moveSpeed to the rb velocity 
    private void FixedUpdate()
    {
        animator.SetFloat("moveSpeed", rigidBody.velocity.magnitude);
    }
}
