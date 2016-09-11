using UnityEngine;
using System.Collections;

public class EnemyCharacter : MonoBehaviour {
    public const float baseSpeed = 3.0f;

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    [SerializeField] private FireController fireController;


    private Animator _animator;
    bool attack;

    private bool _alive;

    void Start()
    {
        _alive = true;
        _animator = GetComponent<Animator>();
        attack = false;
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

                Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);

                if (!hitObject.GetComponent<PlayerCharacter>())
                {
                    _animator.SetBool("Attack", false);
                    if (hit.distance < obstacleRange)
                    {
                        float angle = Random.Range(-110, 110);
                        transform.Rotate(0, angle, 0);
                    }
                }



                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if ((hit.distance < obstacleRange / 4))
                    {
                        _animator.SetBool("Attack", true);
                    }
                    else
                    {
                        _animator.SetBool("Attack", false);

                        foreach (Collider hitCollider in hitColliders)
                        {
                            if (hitCollider.name == "Player")
                            {
                                float[] allies_distance = { 0, 0 };
                                int allies_count = 0;
                                float my_distance = Vector3.Distance(transform.position, hitCollider.transform.position);

                                Vector3 player = new Vector3(hitCollider.gameObject.transform.position.x, transform.position.y, hitCollider.gameObject.transform.position.z);

                                foreach (Collider underhitCollider in hitColliders)
                                {
                                    if (hitCollider.tag == "Enemy")
                                    {
                                        Debug.Log(hitCollider.tag);
                                        allies_distance[allies_count] = Vector3.Distance(player, hitCollider.transform.position);
                                        allies_count++;
                                    }
                                }

                                transform.LookAt(player);

                                if ((allies_count > 0 && my_distance > allies_distance[0]) || (allies_count > 1 && my_distance <= allies_distance[1]))
                                {
                                    float angle = 85 * (my_distance / 5);
                                    //Debug.Log(transform.name + "rotate to " + angle);
                                    transform.Rotate(0, angle * speed * Time.deltaTime, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (Collider hitCollider in hitColliders)
                    {
                        if (hitCollider.name == "Player")
                        {
                            Vector3 player_angle = (transform.eulerAngles - hitCollider.gameObject.transform.localEulerAngles);
                            transform.Rotate(0, player_angle.y * speed * Time.deltaTime, 0);
                        }
                    }
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
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
