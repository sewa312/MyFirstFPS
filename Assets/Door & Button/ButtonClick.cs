using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {
    public GameObject door;
    bool buttonColor;

	// Use this for initialization
	void Start () {
        buttonColor = false;
        GetComponent<Renderer>().material.color = new Color(255,0,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Operate ()
    {
        door.GetComponent<Door>().FunctionOpen();
    }

    public void ChangeColor(bool buttonColor)
    {
        buttonColor = !buttonColor;
        if (buttonColor)
        {
            GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
        else
        {
            GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }
    }
}
