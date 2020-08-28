﻿using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Must;

public class Ball : MonoBehaviour
{
    public float randomFactor;

    // cached values
    private Transform paddleTransform;
    private bool started = false;
    private Vector3 paddleToBallVector;
    private Rigidbody2D thisRigidBody2D;
    private AudioSource hitSound;
    private GameObject previousGameObject = null;
    private Vector2 previousRelativeVelocity;

    // Use this for initialization
    void Start()
    {
        paddleTransform = FindObjectOfType<Paddle>()?.transform;
        paddleToBallVector = transform.position - paddleTransform.position;
        thisRigidBody2D = GetComponent<Rigidbody2D>();
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            //lock the ball to the paddle.
            transform.position = paddleTransform.position + paddleToBallVector;

            // wait for a click to launch.
            if (Input.GetMouseButtonDown(0))
            {
                started = true;
                thisRigidBody2D.velocity = new Vector2(2f, 10f);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (started)
        {
            var collisionObject = collision.gameObject;
            bool isPlayer = collisionObject.CompareTag("Player");
            bool isBoundaries = LayerMask.LayerToName(collisionObject.layer).Equals("Boundaries");
            if (isPlayer || isBoundaries) 
            {
                hitSound.Play();
            }

            if (previousGameObject != null)
            {
                Debug.Log("collision.relativeVelocity - " + collision.relativeVelocity.normalized);
                Vector3 directionBeforeCollision = gameObject.transform.position - previousGameObject.transform.position;
                Debug.Log("directionBeforeCollision - " + directionBeforeCollision.normalized);
            }


            float minYVelocity = 1f;
            // randomize if velocity is inverse to previous one
            if(previousRelativeVelocity == collision.relativeVelocity * -1)
            {
                // randomize direction to prevent getting stuck
                Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), 0f);
                velocityTweak.y = Mathf.Max(velocityTweak.y, minYVelocity);
                thisRigidBody2D.velocity += velocityTweak;
            }
            previousGameObject = collision.gameObject;
            previousRelativeVelocity = new Vector2(collision.relativeVelocity.x, collision.relativeVelocity.y);
        }
    }
}
