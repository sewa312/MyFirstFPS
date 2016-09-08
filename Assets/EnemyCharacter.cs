using UnityEngine;
using System.Collections;

public class EnemyCharacter : MonoBehaviour {
    public const float baseSpeed = 3.0f;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    [SerializeField] private FireController fireController;


    private Animator _animator;


    private bool _alive;

    void Start()
    {
        _alive = true;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetFloat("Speed", speed);
        if (_alive)
        {

            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1.5f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                   /* if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                    */
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
        _animator.SetBool("Alive", false);
    }

    public void ReactToHit()
    {
        fireController.CreateFire(transform.TransformPoint(Vector3.forward));
        SetAlive(false);
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        //this.transform.Rotate(-75, 0, 0);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
