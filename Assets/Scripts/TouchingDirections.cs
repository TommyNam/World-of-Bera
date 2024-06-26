using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    public ContactFilter2D castFilter; // Filter for selecting which layers to check for collisions
    public float groundDistance = 0.05f; // Distance to check below the GameObject for ground
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.5f;

    CapsuleCollider2D touchingCol; // Reference to the CapsuleCollider2D component

    Animator animator; // Reference to the Animator component
    RaycastHit2D[] groundHits = new RaycastHit2D[5]; // Array to store raycast hit results
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private bool _isGrounded; // Private field to store grounded state
    public bool IsGrounded
    {
        get
        {
            return _isGrounded; // Getter for the IsGrounded property
        }
        private set
        {
            _isGrounded = value; // Setter for the IsGrounded property
            animator.SetBool(AnimationStrings.isGrounded, value); // Update the animator parameter
        }
    }

    private bool _isOnWall;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall; // Getter
        }
        private set
        {
            _isOnWall = value; // Setter
            animator.SetBool(AnimationStrings.isOnWall, value); // Update the animator parameter
        }
    }

    public bool _isOnCeiling;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling; // Getter
        }
        private set
        {
            _isOnCeiling = value; // Setter
            animator.SetBool(AnimationStrings.isOnCeiling, value); // Update the animator parameter
        }
    }

    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>(); // Get the CapsuleCollider2D component
        animator = GetComponent<Animator>(); // Get the Animator component
    }

    void FixedUpdate()
    {
        // Cast a ray downwards and check if it hits any colliders within the groundDistance
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }
}
