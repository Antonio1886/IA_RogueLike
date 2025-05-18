using UnityEngine;

public class EnergyEnemyController : MonoBehaviour
{
     public Transform player;
    public float moveSpeed = 3f;
    public float approachRange = 10f;
    public float attackRange = 5f;

    public Transform firePoint;
    public GameObject laserPrefab;

    public float fireDuration = 3f;
    public float cooldownTime = 8f;
    public int numberOfProjectiles = 8;
    public float firePointRotationSpeed = 90f; // grados por segundo

    private float fireTimer;
    private bool isFiring;
    private float cooldownTimer;

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            if (distance < approachRange)
            {
                Vector2 dir = (player.position - transform.position).normalized;
                transform.position += (Vector3)dir * moveSpeed * Time.deltaTime;
            }
            StopFiring();
        }
        else
        {
            if (cooldownTimer <= 0)
            {
                if (!isFiring)
                {
                    isFiring = true;
                    fireTimer = fireDuration;
                }

                HandleFiring();
            }
            else
            {
                cooldownTimer -= Time.deltaTime;
            }
        }

        // Girar el firePoint constantemente como reloj
        firePoint.Rotate(0, 0, -firePointRotationSpeed * Time.deltaTime);
    }

    void HandleFiring()
    {
        fireTimer -= Time.deltaTime;

        if (fireTimer > 0)
        {
            ShootProjectilesInCircle();
            fireTimer = 0; // Solo dispara una vez por ciclo de 3s
        }
        else
        {
            StopFiring();
            cooldownTimer = cooldownTime;
        }
    }

    void ShootProjectilesInCircle()
    {
        float angleStep = 360f / numberOfProjectiles;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle) * firePoint.rotation;
            Instantiate(laserPrefab, firePoint.position, rotation);
        }
    }

    void StopFiring()
    {
        isFiring = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, approachRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
