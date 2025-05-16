using UnityEngine;
using Unity.UI;

public class VoidProjecti : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public float lifetime = 5f;

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameManager.Instance.ShowHitCanvas();
            Destroy(gameObject);
        }
    }
}
