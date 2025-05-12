using UnityEngine;

public class golpearma : MonoBehaviour
{
    private Animator animator;
    public float cooldownTime = 1.0f; // Tiempo de cooldown en segundos
    private float nextGolpeTime = 0f; // Tiempo en que se podr� ejecutar el pr�ximo golpe

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifica si se presion� el bot�n y si ha pasado el tiempo de cooldown
        if (Input.GetMouseButtonDown(0) && Time.time >= nextGolpeTime)
        {
            animator.SetTrigger("Golpear");
            // Establece el pr�ximo tiempo disponible para golpear
            nextGolpeTime = Time.time + cooldownTime;
        }
    }
}