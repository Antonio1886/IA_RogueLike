using UnityEngine;

public class VerdugoController : MonoBehaviour
{
    public Transform target;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float detectionRange = 10f;
    public float moveSpeed = 2f;
    public float stopDistance = 3f;
    public float pauseBeforeShoot = 2f;

    private enum State { Idle, Chasing, Waiting, Shooting }
    private State currentState = State.Idle;

    private float waitStartTime;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        // Si el jugador está fuera del rango azul, se detiene completamente
        if (distance > detectionRange)
        {
            currentState = State.Idle;
            return;
        }

        switch (currentState)
        {
            case State.Idle:
                if (distance <= detectionRange && distance > stopDistance)
                {
                    currentState = State.Chasing;
                }
                break;

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
                ShootBoomerang();
                currentState = State.Chasing;
                break;
        }
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void ShootBoomerang()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        AxeProjectil boomerang = projectile.GetComponent<AxeProjectil>();

        if (boomerang != null)
        {
            boomerang.SetTarget(transform); // volverá al verdugo
        }

        Debug.Log("Disparo boomerang");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange); // rango de detección

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance); // rango para detenerse y atacar
    }
}
