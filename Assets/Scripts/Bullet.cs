using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int power;

    Vector2 currentVelocity = Vector2.zero;
    float currentAngularVelocity = 0f;

    bool inCollision = false;

    Rigidbody2D physics;

    bool paused = false;

    private void Awake()
    {
        physics = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCollision && !paused)
        {
            currentVelocity = physics.velocity;
            currentAngularVelocity = physics.angularVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!paused)
        {
            inCollision = true;
            Vector2 velocity = currentVelocity;
            Vector2 reflectAxis = collision.transform.up;
            Vector2 reflectedVelocity = Vector2.Reflect(velocity, reflectAxis);
            //Debug.Log("Current Velocity: " + velocity);
            //Debug.Log("Reflection Axis: " + reflectAxis);
            //Debug.Log("Reflected Velocity: " + reflectedVelocity);
            physics.velocity = reflectedVelocity;
            inCollision = false;

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Player>().UpdateHealth(power);
            }
        }
    }

    public void Pause(bool pause)
    {
        paused = pause;
        if (pause)
        {
            
            physics.velocity = Vector2.zero;
            physics.angularVelocity = 0f;
            physics.isKinematic = true;
        }
        else
        {
            physics.velocity = currentVelocity;
            physics.angularVelocity = currentAngularVelocity;
            physics.isKinematic = false;
        }
    }

    public void InitializeVelocity(Vector2 velocity)
    {
        paused = true;
        physics.isKinematic = true;
        currentVelocity = velocity;
    }
}
