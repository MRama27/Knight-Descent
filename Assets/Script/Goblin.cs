using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class Goblin : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public float walkStopRate = 0.5f;
    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;
    Damageable damageable;

    public enum WaklableDirection { Right, Left }
    private WaklableDirection _walkDirection;
    private Vector2 WalkDirectionVector = Vector2.right;


    public WaklableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WaklableDirection.Right)
                {
                    WalkDirectionVector = Vector2.right;
                }
                else if (value == WaklableDirection.Left)
                {
                    WalkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }
    
    public bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown { get {
        return animator.GetFloat(AnimationStrings.attackCooldown);
    } private set {
        animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));
    } }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

       void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

        if(AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
       if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if(!damageable.LockVelocity)
        {
        if(CanMove) 
            
            rb.linearVelocity = new Vector2( 
                Mathf.Clamp(rb.linearVelocity.x + (walkAcceleration * WalkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), 
                rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, 0, walkStopRate), rb.linearVelocity.y);
        }
    }

    private void FlipDirection()
    {
        if (WalkDirection == WaklableDirection.Right)
        {
            WalkDirection = WaklableDirection.Left;
        } else if (WalkDirection == WaklableDirection.Left)
        {
            WalkDirection = WaklableDirection.Right;
        } else
        {
            Debug.LogError("Current walkable direction is not set to legal values of right or left");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }
    // Update is called once per frame
    public void OnCliffDetected()
    {
        if(touchingDirections.IsGrounded)
        {
            FlipDirection();
        }
    }
}
