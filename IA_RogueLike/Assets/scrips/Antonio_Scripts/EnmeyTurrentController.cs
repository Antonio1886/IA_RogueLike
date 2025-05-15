using UnityEngine;

public class  EnmeyTurrentController : MonoBehaviour
{
    public Transform target;
    public GameObject projectilePrefab;
    public Transform firePoint;

    public float attackRange = 8f;
    public float fireRate = 0.2f;             // Tiempo entre disparos individuales
    public int shotsPerBurst = 3;             // Cantidad de disparos por ráfaga
    public float burstCooldown = 2f;          // Tiempo entre ráfagas

    private bool isShooting = false;
    private float nextBurstTime = 0f;

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= attackRange && Time.time >= nextBurstTime && !isShooting)
        {
            StartCoroutine(FireBurst());
        }
    }

    private System.Collections.IEnumerator FireBurst()
    {
        isShooting = true;

        for (int i = 0; i < shotsPerBurst; i++)
        {
            if (projectilePrefab != null && firePoint != null && target != null)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Vector3 direction = (target.position - firePoint.position).normalized;

                SimpleProjectile proj = projectile.GetComponent<SimpleProjectile>();
                if (proj != null)
                {
                    proj.SetDirection(direction);
                }
            }

            yield return new WaitForSeconds(fireRate);
        }

        nextBurstTime = Time.time + burstCooldown;
        isShooting = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
