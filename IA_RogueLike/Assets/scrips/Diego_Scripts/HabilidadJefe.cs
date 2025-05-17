using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{
    [SerializeField] private float daño;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private Transform posicionCaja;
    [SerializeField] private float tiempoDeVida;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

        private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);

        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<Health>().TomarDaño(daño);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(posicionCaja.position, dimensionesCaja);
    }
}
