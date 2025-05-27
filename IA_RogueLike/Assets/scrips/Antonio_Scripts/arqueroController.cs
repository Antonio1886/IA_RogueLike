using UnityEngine;

public class ArqueroController : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target;
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Configuración")]
    public float detectionRange = 10f;
    public float moveSpeed = 2f;
    public float stopDistance = 3f;
    public float pauseBeforeShoot = 2f;
    public LayerMask obstacleLayers;

    private enum State { Chasing, Waiting, Shooting }
    private State currentState = State.Chasing;
    private float waitStartTime;
    private bool playerInRange = false;

    private void Start()
    {
        // Buscar al jugador de forma más segura
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró objeto con tag 'Player'");
            enabled = false; // Desactiva este script
        }
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector2.Distance(transform.position, target.position);
        playerInRange = distance <= detectionRange;

        if (!playerInRange) return;

        // Verificar línea de visión
        bool canSeePlayer = CanSeePlayer();

        if (!canSeePlayer)
        {
            currentState = State.Chasing;
            return;
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
                ShootAtTarget();
                currentState = State.Chasing;
                break;
        }

    }

    private bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            firePoint.position,
            target.position - firePoint.position,
            detectionRange,
            obstacleLayers);

        return hit.collider == null || hit.collider.CompareTag("Player");
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector2 direction = (destination - transform.position).normalized;
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }



    private void ShootAtTarget()
    {
        if (projectilePrefab == null || firePoint == null || target == null) return;

        Vector2 direction = (target.position - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.Euler(0, 0, angle));

        SimpleProjectile proj = projectile.GetComponent<SimpleProjectile>();
        if (proj != null)
        {
            proj.SetDirection(direction);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        if (target != null && firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(firePoint.position, target.position);
        }
    }
}