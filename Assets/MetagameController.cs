using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MetagameController : MonoBehaviour {

    [SerializeField]
    private Text winOrFail;


    void Awake()
    {
        Messenger.AddListener(GameEvent.WIN, Win);
        Messenger.AddListener(GameEvent.FAIL, Fail);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WIN, Win);
        Messenger.RemoveListener(GameEvent.FAIL, Fail);
    }


    // Use this for initialization
    void Win () {
        winOrFail.text = "You WIN!";
        Application.LoadLevel("winmenu");
    }
	
	// Update is called once per frame
	void Fail () {
        winOrFail.text = "You DIED!";
        Application.LoadLevel("failmenu");
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
