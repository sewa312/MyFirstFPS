using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class PlayerCharacter : MonoBehaviour {
    public float radius = 1.5f;
    public int timeToDeath = 500;
    Stopwatch timer;
    [SerializeField] private UIController _hintControl;


    private bool alive;

	// Use this for initialization
	void Start () {
        timer = new Stopwatch();
        alive = true;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hitCollider in hitColliders)
            {
                hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void OnTriggerStay(Collider collider)
    {
        timer.Start();
        if (timer.ElapsedMilliseconds >= timeToDeath)
        {
            Messenger.Broadcast(GameEvent.FAIL);
            Death();
        }
    }

    public void OnTriggerExit(Collider colleder)
    {
        timer.Reset();
    }

    public void Death()
    {
        alive = false;
        UnityEngine.Debug.Log("Death ");
        // Отправить сообщение о смерти в gamecontroller
    }
}
