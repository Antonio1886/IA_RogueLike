using UnityEngine;

public class windProjctil : MonoBehaviour
{
       public float speed = 10f;
    public float lifetime = 5f;
    public float knockbackForce = 500f; // Puedes ajustar este valor

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.collider.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                // Dirección del retroceso desde la bala hacia el jugador
                Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;

                // Aplica la fuerza de retroceso
                playerRb.AddForce(knockbackDir * knockbackForce);
            }

            Destroy(gameObject);
        }
    }
}
