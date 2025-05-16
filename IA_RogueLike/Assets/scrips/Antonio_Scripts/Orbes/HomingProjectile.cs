using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
public float speed = 5f;
    public float lifeTime = 4f;
    private Transform target;
    private float spawnTime;

    void Start()
    {
        // Busca el objeto con la etiqueta "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }

        spawnTime = Time.time;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        if (Time.time - spawnTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
