using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 5f;

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

    void OnTriggerEnter(Collider other)
    {
        Health targetHealth = other.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
