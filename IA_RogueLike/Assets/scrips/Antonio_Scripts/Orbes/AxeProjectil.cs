using UnityEngine;

public class AxeProjectil : MonoBehaviour
{
public float speed = 5f;
    public float forwardDuration = 1f;
    public float returnSpeed = 7f;
    public float destroyDistance = 0.3f;

    private Transform returnTarget;
    private bool isReturning = false;
    private float timer;

    public void SetTarget(Transform target)
    {
        returnTarget = target;
    }

    void Start()
    {
        timer = forwardDuration;
    }

    void Update()
    {
        if (!isReturning)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                isReturning = true;
            }
        }
        else
        {
            if (returnTarget != null)
            {
                Vector2 dir = ((Vector2)returnTarget.position - (Vector2)transform.position).normalized;
                transform.position += (Vector3)(dir * returnSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, returnTarget.position) < destroyDistance)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
