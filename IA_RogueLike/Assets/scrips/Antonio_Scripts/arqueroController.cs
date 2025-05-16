using UnityEngine;

public class arqueroController : MonoBehaviour
{
    public Transform target;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float detectionRange = 10f;
    public float moveSpeed = 2f;
    public float stopDistance = 3f;
    public float pauseBeforeShoot = 2f;

    private enum State { Chasing, Waiting, Shooting }
    private State currentState = State.Chasing;

    private float waitStartTime;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        switch (currentState)
        {
            case State.Chasing:
                if (distance > stopDistance)
                {
                    MoveTowards(target.position);
                }
                else
                {
                    currentState = State.Waiting;
                    waitStartTime = Time.time;
                }
                break;

            case State.Waiting:
                if (Time.time - waitStartTime >= pauseBeforeShoot)
                {
                    currentState = State.Shooting;
                }
                break;

            case State.Shooting:
                ShootAtTarget();
                currentState = State.Chasing;
                break;
        }
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

private void ShootAtTarget()
{
    if (projectilePrefab == null || firePoint == null) return;

    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
    Vector3 direction = (target.position - firePoint.position).normalized;

    SimpleProjectile proj = projectile.GetComponent<SimpleProjectile>();
    if (proj != null)
    {
        proj.SetDirection(direction);
    }

    Debug.Log("Disparo con dirección");
}


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
