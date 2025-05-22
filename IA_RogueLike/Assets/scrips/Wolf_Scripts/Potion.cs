using Unity.VisualScripting;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [Header("Cura")]
        [SerializeField] private float cantidadCura;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Jugador Curado");
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Health>().Curarse(cantidadCura);
        }
    }
}
