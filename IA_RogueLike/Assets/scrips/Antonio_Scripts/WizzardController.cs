using UnityEngine;

public class WizzardController : MonoBehaviour
{
  public Transform target;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float detectionRange = 10f;
    public float moveSpeed = 2f;
    public float stopDistance = 3f;
    public float pauseBeforeShoot = 2f;
    public float projectileSpeed = 5f;
    public int circleSegments = 12;

    private enum State { Chasing, Waiting, Shooting }
    private State currentState = State.Chasing;

    private enum PatternType { Triangle, Circle, Square }
    private PatternType currentPattern = PatternType.Triangle;

    private float waitStartTime;

  void Update()
{
    if (target == null) return;

    float distance = Vector3.Distance(transform.position, target.position);

    // Solo actúa si el jugador está dentro del rango de detección (gizmo azul)
    if (distance > detectionRange)
    {
        return; // No hace nada, se queda estático
    }

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
            ShootPattern();
            CyclePattern();
            currentState = State.Chasing;
            break;
    }
}
    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void ShootPattern()
    {
        switch (currentPattern)
        {
            case PatternType.Triangle:
                FireShapePattern(3);
                break;
            case PatternType.Circle:
                FireShapePattern(circleSegments);
                break;
            case PatternType.Square:
                FireSquarePattern();
                break;
        }
    }

    private void FireShapePattern(int originalCount)
{
    int numProjectiles = Mathf.CeilToInt(originalCount * 0.8f);
    float angleStep = 360f / numProjectiles;

    for (int i = 0; i < numProjectiles; i++)
    {
        float angle = i * angleStep * Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
        SpawnProjectile(dir);
    }
}


    private void FireSquarePattern()
    {
        Vector3[] directions = {
            Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            (Vector3.up + Vector3.right).normalized,
            (Vector3.up + Vector3.left).normalized,
            (Vector3.down + Vector3.right).normalized,
            (Vector3.down + Vector3.left).normalized
        };

        foreach (Vector3 dir in directions)
        {
            SpawnProjectile(dir);
        }
    }

    private void SpawnProjectile(Vector3 direction)
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        SimpleProjectile proj = projectile.GetComponent<SimpleProjectile>();
        if (proj != null)
        {
            proj.SetDirection(direction.normalized);
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }
    }

    private void CyclePattern()
    {
        currentPattern = (PatternType)(((int)currentPattern + 1) % System.Enum.GetValues(typeof(PatternType)).Length);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
    }
}
