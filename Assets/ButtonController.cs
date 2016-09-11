using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;

	//
	public void OneMouseUp () {
	    if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
	}
}
