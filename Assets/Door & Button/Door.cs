using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Door : MonoBehaviour
{
    [SerializeField] private AudioSource soundSourse;
    [SerializeField] private AudioClip door;
    public AnimationClip a_open;
    public AnimationClip a_close;

    private bool _open;
    private bool _completed;

    Stopwatch close;
    int timeToClose = 3000;

    public GameObject button;

    // Use this for initialization
    void Start()
    {
        _open = true;
        _completed = true;
        close = new Stopwatch();
    }

    public void Update()
    {
        if (_open)
        {
            if (!_completed)
            {
                GetComponent<Animation>().CrossFade(a_close.name);
                _completed = true;
                close.Reset();
            }
        }
        else
        {
            if (!_completed)
            {
                GetComponent<Animation>().CrossFade(a_open.name);
                _completed = true;
                close.Start();
            }
        }

        if (close.ElapsedMilliseconds >= timeToClose)
        {
            FunctionOpen();
            close.Reset();
        }
    }

    public void FunctionOpen()
    {
        _open = !_open;
        button.GetComponent<ButtonClick>().ChangeColor(_open);
        _completed = false;
        soundSourse.PlayOneShot(door);
    }
}
