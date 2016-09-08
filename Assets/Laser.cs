using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    private Camera _camera;
    public Transform laser;
    public Transform player;
    public Transform startPoint;
    public Transform gun;
    private Vector3 defPos;
    private Vector3 defScale;

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
        defPos = laser.localPosition;
        defScale = laser.localScale;
        laser.gameObject.layer = 2;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(point);



        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButton(0))
            {
                //Debug.Log("wiewpoint: x " + hit.point.x + " y " + hit.point.y + " z " + hit.point.z);
                float distance = Vector3.Distance(startPoint.position, hit.point);
                Vector3 center = (startPoint.position + hit.point) / 2;

                RaycastHit lineHit;
                //if (Physics.Linecast(startPoint.position, hit.point, out lineHit))
                {
                    //    distance = Vector3.Distance(startPoint.position, lineHit.point);
                    //   center = (startPoint.position + lineHit.point) / 2;
                }

                //Vector3 gunLook = hit.point;
                // g/unLook.y = (startPoint.position.y - hit.point.y) ;
                //Vector3 lookTo = new Vector3(hit.point.x, hit.point.y - 90, hit.point.z);
                //playerGun.LookAt(gunLook);

                laser.localScale = new Vector3(laser.localScale.x, distance / 2, laser.localScale.z);
                laser.position = center;
                laser.localPosition = new Vector3(defPos.x, defPos.y, laser.localPosition.z);
            }
            else
            {
                laser.localPosition = defPos;
                laser.localScale = defScale;
            }
        }
    }
}
