using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]

public class Player : MonoBehaviour
{
    int score;

    int health = 100;

    //bool movesAllowed = true;

    public float moveSpeed = 2f;
    public float maxSpeed = 5f;

    protected bool dead;
    protected bool dying = false;

    public float cooldown = 1f;
    public float finalCooldown = 0.5f;
    public float nextFire = 0.0f;

    protected float origMaxSpeed;
    protected float origMoveSpeed;

    protected GameManager gameManager;

    public Bullet bullet;

    public Vector2 bulletSpeed = new Vector2(5, 5);

    public float bulletSpawnOffset = 2f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        origMaxSpeed = maxSpeed;
        origMoveSpeed = moveSpeed;
    }

    void Move(){
         Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0) {
            if (Mathf.Abs(velocity.x) < maxSpeed)
                velocity += Vector2.right * Input.GetAxisRaw("Horizontal");
            else
                velocity = new Vector2(Mathf.Sign(velocity.x) * maxSpeed, velocity.y);
        }
        else {
            velocity = new Vector2(0f, velocity.y);
        }
        if(Mathf.Sign(velocity.x) != Mathf.Sign(Input.GetAxisRaw("Horizontal"))){
            velocity = new Vector2(-velocity.x, velocity.y);
        }
        if (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0) {
            if (Mathf.Abs(velocity.y) < maxSpeed)
                velocity += Vector2.up * Input.GetAxisRaw("Vertical");
            else
                velocity = new Vector2(velocity.x, Mathf.Sign(velocity.y) * maxSpeed);
        }
        else {
            velocity = new Vector2(velocity.x, 0f);
        }
        velocity = velocity.normalized * moveSpeed;
        GetComponent<Rigidbody2D>().velocity = velocity;
        if (velocity == Vector2.zero) {
            //GetComponent<Animator>().SetTrigger("idle");
        }
        if (Input.GetAxis("Vertical") != 0) {
            //GetComponent<Animator>().SetInteger("direction", (int)Mathf.Sign(Input.GetAxis("Vertical")));
            //GetComponent<Animator>().SetTrigger("move");
        }
        if (Input.GetAxis("Horizontal") != 0) {
            //GetComponent<Animator>().SetInteger("direction", 1);
            //GetComponent<Animator>().SetTrigger("move");
        }
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        if (h == 0 && v == 0) {
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - (transform.eulerAngles.z % 45));
        }
        if (h > 0 && v == 0) {
            //if (GetComponent<Animator>().GetInteger("direction") > 0)
                transform.eulerAngles = new Vector3(0, 0, -90 * Mathf.Sign(Input.GetAxis("Horizontal")));
            //else if (GetComponent<Animator>().GetInteger("direction") < 0)
            //    transform.eulerAngles = new Vector3(0, 0, 90 * Mathf.Sign(Input.GetAxis("Horizontal")));
        }
        else if (h < 0 && v == 0) {
            //if (GetComponent<Animator>().GetInteger("direction") > 0)
                transform.eulerAngles = new Vector3(0, 0, -90 * Mathf.Sign(Input.GetAxis("Horizontal")));
            //else if (GetComponent<Animator>().GetInteger("direction") < 0)
            //    transform.eulerAngles = new Vector3(0, 0, 90 * Mathf.Sign(Input.GetAxis("Horizontal")));
        }
        if (h == 0 && v > 0) {
            transform.rotation = Quaternion.identity;
        }
        else if (h == 0 && v < 0) {
            transform.rotation = Quaternion.identity;
        }
        else if (h > 0 && v > 0) {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
        else if (h > 0 && v < 0) {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else if (h < 0 && v > 0) {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else if (h < 0 && v < 0) {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            if(gameManager.timePaused){
                Move();
            }
            if (gameManager.timePaused && Input.GetButtonDown("Fire")) {
                Fire();
            }
            if(Input.GetButtonDown("Switch Turn")){
                gameManager.toggleTime();
            }
            if (!gameManager.timePaused)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    void Kill(){ 
        Debug.LogError("You Died");
    }
    
    void Fire(){
        Vector2 vvector = transform.up;
        float offset = bulletSpawnOffset;
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
            vvector = new Vector2(vvector.x, vvector.y * Input.GetAxis("Vertical"));
            offset *= Input.GetAxis("Vertical");
        }
        Bullet b = Instantiate(bullet, transform.position + transform.up * offset, Quaternion.identity) as Bullet;
        
        vvector.Scale(bulletSpeed);
        //b.GetComponent<Rigidbody2D>().velocity = vvector;
        b.InitializeVelocity(vvector);
    }

    public void UpdateScore(int scoreAmount){
        score += scoreAmount;
    }

    public void UpdateHealth(int healthAmount){
        health -= healthAmount;
        if(health <= 0){
            Kill();
        }
    }

    //public void startTurn(){
    //    movesAllowed = true;
    //}

    //public void endTurn(){
    //    movesAllowed = false;
    //}


}
