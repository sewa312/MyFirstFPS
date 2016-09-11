using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private int[] Enemies = { 1, 1, 1 };
    private int count = 3;
    private bool playerAlive = true;

    void Awake()
    {
        //Messenger.AddListener(GameEvent.WIN, GameEnding);
        //Messenger.AddListener(GameEvent.FAIL, GameEnding);
        Messenger.AddListener(GameEvent.ENEMY_DEATH, MinusEnemy);
        Messenger.AddListener(GameEvent.PLAYER_DEATH, GameEnding);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DEATH, GameEnding);
        Messenger.RemoveListener(GameEvent.ENEMY_DEATH, MinusEnemy);
    }

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GameEnding()
    {
        playerAlive = false;
        Messenger.Broadcast(GameEvent.FAIL);
        Debug.Log("FAIL");
    }

    public void MinusEnemy()
    {
        if (Enemies[0] != 0)
        {
            Enemies[0] = 0;
            count--;
            Debug.Log("d 1");

        } else if (Enemies[1] != 0)
        {
            Enemies[1] = 0;
            count--;
            Debug.Log("d 2");
        } else if (Enemies[2] != 0)
        {
            Enemies[2] = 0;
            count--;
            Debug.Log("d 3");
        }
        if (count == 0)
        {
            Debug.Log("WIN");
            Messenger.Broadcast(GameEvent.WIN);
        }
    }


    public void Restart()
    {
        Application.LoadLevel("scene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
