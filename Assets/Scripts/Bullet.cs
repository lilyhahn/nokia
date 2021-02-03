using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int power;

    Vector2 currentVelocity = Vector2.zero;

    bool inCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!inCollision)
        {
            currentVelocity = GetComponent<Rigidbody2D>().velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        inCollision = true;
        Vector2 velocity = currentVelocity;
        Vector2 reflectAxis = collision.transform.up;
        Vector2 reflectedVelocity = Vector2.Reflect(velocity, reflectAxis);
        Debug.Log("Current Velocity: " + velocity);
        Debug.Log("Reflection Axis: " + reflectAxis);
        Debug.Log("Reflected Velocity: " + reflectedVelocity);
        GetComponent<Rigidbody2D>().velocity = reflectedVelocity;
        inCollision = false;
    }
}
