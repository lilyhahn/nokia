using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public const int PlayerTurnDef = 0;
    //public const int EnemyTurnDef = 1;

    public List<Level> levels;

    int levelIndex = 0;

    public Player player;

    Level currentLevel = null;

    public bool paused {get; private set; }

    public float pauseDelay = 0.5f;
    public float timeScaleMin = 0.25f;

    //int turn = 0; // 0 - player turn, 1 - enemy turn

    public bool timePaused { get; private set; }

    bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        loadLevel(levelIndex);
        paused = false;
        toggleTime();
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
        //startTurn(PlayerTurnDef);
    }

    public void toggleTime()
    {
        StartCoroutine(DelayPause(!timePaused));
        
    }

    IEnumerator DelayPause(bool direction)
    {
        if (isRunning)
        {
            yield break; ///exit if this is still running
        }
        isRunning = true;

        float counter = 0;

        float start = Time.timeScale;

        float end;

        float duration = pauseDelay;

        if (direction)
        {
            end = timeScaleMin;
        }
        else
        {
            end = 1;
            duration /= 4;
        }

        while(counter < duration)
        {
            counter += Time.deltaTime;
            float slowTime = Mathf.Lerp(start, end, counter / duration);
            Time.timeScale = slowTime;
            Time.fixedDeltaTime = slowTime * 0.02f;
            yield return null;
        }

        isRunning = false;
        timePaused = !timePaused;

        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            bullet.GetComponent<Bullet>().Pause(timePaused);
        }
        if (timePaused)
        {
            Debug.Log("Time Paused");
        }
        else
        {
            Debug.Log("Time Resumed");
        }
    }

    //public void startTurn(int TurnDef){
    //    switch(TurnDef){
    //        case PlayerTurnDef:
    //            foreach(Enemy e in currentLevel.enemies){
    //                e.endTurn();
    //            }
    //            player.startTurn();
    //        break;
    //        case EnemyTurnDef:
    //            foreach(Enemy e in currentLevel.enemies){
    //                e.startTurn();
    //            }
    //            player.endTurn();
    //        break;
    //    }
    //}
}
