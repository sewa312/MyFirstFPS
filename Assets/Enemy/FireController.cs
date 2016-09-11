using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class FireController : MonoBehaviour {
    private bool fireInScene;
    private GameObject _fire;
    [SerializeField] private GameObject fire;
    Stopwatch lifeTime;
    public float LifeTime = 1.5f;
    private float RespawnTime = 0;

    private void Start()
    {
        fireInScene = false;
        lifeTime = new Stopwatch();
    }

	// Use this for initialization
	public void CreateFire (Vector3 place) {
        if (!fireInScene)
        {
            fireInScene = true;
            _fire = Instantiate(fire) as GameObject;
            _fire.transform.position = transform.TransformPoint(place);
            lifeTime.Start();
        }
    }
	
	public void DestroyFire () {
        fireInScene = false;
    }

    void Update()
    {
        if (fireInScene && _fire != null)
        {
            RespawnTime += Time.deltaTime;
            if (RespawnTime > LifeTime)
            {
                DestroyFire();
                Messenger.Broadcast(GameEvent.ENEMY_DEATH);
                Destroy(gameObject);
            }
        }
        else
        {
            RespawnTime = 0;
        }
    }
}
