using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aquí puedes agregar lógica adicional (daño, efectos, etc.)
        Destroy(gameObject);
    }
}
