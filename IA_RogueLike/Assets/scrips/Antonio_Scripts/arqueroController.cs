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

    // Verificar si el jugador está dentro del rango de detección
    if (distance > detectionRange)
    {
        // Si está fuera del rango, el arquero no hace nada
        return;
    }

    // Lógica existente del estado del arquero
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
    if (projectilePrefab == null || firePoint == null || target == null) return;

    // Dirección en 2D (ignoramos el eje Z)
    Vector2 direction = (target.position - firePoint.position).normalized;

    // Calcular ángulo en 2D
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    // Crear proyectil con rotación para que apunte en la dirección del disparo
    GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.Euler(0, 0, angle));

    // Establecer dirección al componente del proyectil
    SimpleProjectile proj = projectile.GetComponent<SimpleProjectile>();
    if (proj != null)
    {
        proj.SetDirection(direction);
    }

    Debug.Log("Disparo en dirección 2D: " + direction);
}
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
