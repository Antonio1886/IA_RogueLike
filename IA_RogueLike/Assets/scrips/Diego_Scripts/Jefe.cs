using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform target;
    private bool mirandoDerecha = true;

    [Header("Vida")]
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraDeVida;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraDeVida.InicializarBarraDeVida(vida);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("No se encontró objeto con tag 'Player'");
            enabled = false; // Desactiva este script
        }
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, target.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if ((target.position.x > transform.position.x && !mirandoDerecha) || (target.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    private void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<Health>().TomarDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }
}
