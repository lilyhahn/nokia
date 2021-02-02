using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool movesAllowed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTurn(){
        movesAllowed = true;
    }
    
    public void endTurn(){
        movesAllowed = false;
    }
}
