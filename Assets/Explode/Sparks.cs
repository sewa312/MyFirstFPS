using UnityEngine;
using System.Collections;

public class Sparks : MonoBehaviour {

    public float LifeTime = 0.3f;
    private float RespawnTime = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        RespawnTime += Time.deltaTime;
        if (RespawnTime > LifeTime)
        {
            Destroy(gameObject);
        }
	}
}
