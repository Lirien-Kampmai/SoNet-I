using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    [SerializeField] private float gravity;
    [SerializeField] private float reboundSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float gravityOffset;
    [SerializeField] private float rotationSpeed;


    private Vector3 velocity;
    private bool    UseGravity;

    private void Awake()
    {
        velocity.x = -Math.Sign(transform.position.x) * horizontalSpeed;
    }

    private void Update()
    {
        Move();
        TryEnabledGravity();
    }

    private void TryEnabledGravity()
    {
        if (Math.Abs(transform.position.x) <= Math.Abs(LevelBoundary.Instance.leftBorder) - gravityOffset)
        {
            UseGravity = true;
        }
    }

    private void Move()
    {
        if (UseGravity == true)
        {
            velocity.y -= gravity * Time.deltaTime;
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
        transform.position += velocity * Time.deltaTime;
        velocity.x = Math.Sign(velocity.x) * horizontalSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LevelEdge levelEdge = collision.GetComponent<LevelEdge>();

        if (levelEdge != null)
        {
            if (levelEdge.Type == EdgeType.Bottom)
            {
                velocity.y = reboundSpeed;
            }
            if (levelEdge.Type == EdgeType.Left && velocity.x < 0 || levelEdge.Type == EdgeType.Right && velocity.x > 0)
            {
                velocity.x *= -1;
            }
        }
    }

    public void AddVerticalVelocity(float velocity)
    {
        this.velocity.y += velocity;
    }

    public void SetHorizontalDirection(float direction)
    {
        velocity.x = Math.Sign(direction) * horizontalSpeed;
    }
}
