using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public Transform player;
    [SerializeField] private Text text;

    private string defaultText = "";
    private bool showText;

    [SerializeField]
    private Text winOrFail;
    private string defaultTextWoF = "";

    //[SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    public float radius = 1.5f;

    public object Messenger { get; private set; }

    // Use this for initialization
    void Start () {
        showText = false;
    }
	
	// Update is called once per frame
	void Update () {
        bool findButton = false;
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, radius);

        foreach (Collider hitCollider in hitColliders)
        {

            Vector3 direction = hitCollider.transform.position - transform.position;
            if (Vector3.Dot(transform.forward, direction) > .5f && hitCollider.name == "Button")
            {
                ShowHint();
                findButton = true;
            }
        }
        if (!findButton)
        {
            UnShowHint();
        }

        if (showText)
        {
            text.text = "LShift to open";
        }
        else
        {
            text.text = defaultText;
        }
	}

    public void ShowHint()
    {
        showText = true;
    }

    public void UnShowHint()
    {
        showText = false;
    }
}
