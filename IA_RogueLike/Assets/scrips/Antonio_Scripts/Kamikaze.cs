using UnityEngine;

public class Kamikaze : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 10f;
    public float moveSpeed = 5f;
    public float explosionDelay = 2f;
    public float explosionRadius = 3f;
    public int explosionDamage = 30;

    private bool isCharging = false;
    private Vector3 moveDirection;
    private float explosionTime;

    void Update()
    {
        if (isCharging)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            if (Time.time >= explosionTime)
            {
                Explode();
            }
        }
        else
        {
            if (target != null && Vector3.Distance(transform.position, target.position) <= detectionRange)
            {
                moveDirection = (target.position - transform.position).normalized;
                explosionTime = Time.time + explosionDelay;
                isCharging = true;
            }
        }
    }

    private void Explode()
    {
        Debug.Log("¡BOOM!");

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hit in hitColliders)
        {
            Health h = hit.GetComponent<Health>();
            if (h != null)
            {
                //h.TakeDamage(explosionDamage);
            }
        }

        // Puedes agregar aquí efectos visuales o sonidos antes de destruirlo
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
