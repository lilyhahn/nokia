using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int PlayerTurnDef = 0;
    public const int EnemyTurnDef = 1;

    public List<Level> levels;

    int levelIndex = 0;

    public Player player;

    Level currentLevel = null;

    public bool paused {get; private set; }

    int turn = 0; // 0 - player turn, 1 - enemy turn


    // Start is called before the first frame update
    void Start()
    {
        loadLevel(levelIndex);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextLevel(){
        levelIndex++;
        loadLevel(levelIndex);
    }

    void loadLevel(int index){
        if(currentLevel != null){
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[index]) as Level;
        player.transform.position = currentLevel.playerStart.position;
        startTurn(PlayerTurnDef);
    }

    public void startTurn(int TurnDef){
        switch(TurnDef){
            case PlayerTurnDef:
                foreach(Enemy e in currentLevel.enemies){
                    e.endTurn();
                }
                player.startTurn();
            break;
            case EnemyTurnDef:
                foreach(Enemy e in currentLevel.enemies){
                    e.startTurn();
                }
                player.endTurn();
            break;
        }
    }
}
