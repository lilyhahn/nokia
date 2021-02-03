using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool movesAllowed = false;

    public int health { get; private set; }

    public float killTime = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int hits)
    {
        health -= hits;
        if(health <= 0)
        {
            Kill();
        }
    }

    void Kill()
    {
        StartCoroutine(KillRoutine());
    }

    IEnumerator KillRoutine()
    {
        // play kill anim or fade out todo
        GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(killTime);
        gameObject.SetActive(false);
    }

    public void startTurn(){
        movesAllowed = true;
    }
    
    public void endTurn(){
        movesAllowed = false;
    }
}
