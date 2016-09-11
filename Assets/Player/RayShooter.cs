using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Diagnostics;

public class RayShooter : MonoBehaviour
{
    [SerializeField] private AudioSource soundSourse;
    [SerializeField] private AudioClip laserShoot;

    private Camera _camera;

    Stopwatch timer;
    private int timeToDeath = 3000;

    void Start()
    {
        _camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        timer = new Stopwatch();
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            soundSourse.PlayOneShot(laserShoot);
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                EnemyCharacter target = hitObject.GetComponent<EnemyCharacter>();
                if (target != null)
                {
                    //UnityEngine.Debug.Log(target.name);
                    timer.Start();
                    if (timer.ElapsedMilliseconds >= timeToDeath)
                    {
                        target.ReactToHit();
                        timer.Reset();
                    }
                }
                else
                {
                    timer.Reset();
                }
            }
            else
            {
                timer.Reset();
            }
        }
        else
        {
            soundSourse.Stop();
            timer.Reset();
        }
    }
}