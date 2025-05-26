using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"El proyectil chocó con: {other.name}");

        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TomarDaño(damage);
            Destroy(gameObject);
        }
        else if (!other.isTrigger) // Si no es un trigger, destruye el proyectil (por ejemplo, una pared)
        {
            Destroy(gameObject);
        }
    }
}


