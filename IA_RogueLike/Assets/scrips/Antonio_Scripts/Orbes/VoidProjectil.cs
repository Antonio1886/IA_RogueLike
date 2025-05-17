using UnityEngine;

public class VoidProjectil : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 5f;
    public Rigidbody2D rb;
    public float lifetime = 5f;

    [Header("Efecto de Ceguera")]
    public float blindnessDuration = 3f;


    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // 1. Buscar el componente en el jugador
            BlindnessEffect blindnessEffect = collision.collider.GetComponent<BlindnessEffect>();
            
            // 2. Aplicar efecto si existe
            if (blindnessEffect != null)
            {
                blindnessEffect.TriggerBlindness(blindnessDuration);
            }

            // 4. Destruir el proyectil
            Destroy(gameObject);
        }
    }
}
