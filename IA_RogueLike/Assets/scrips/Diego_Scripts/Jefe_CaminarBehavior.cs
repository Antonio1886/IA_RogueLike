using UnityEngine;

public class Jefe_CaminarBehavior : StateMachineBehaviour
{
    private Jefe jefe;
    private Rigidbody2D rb2D;
    private Transform jugador;

    [SerializeField] private float velocidadMovimiento;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<Jefe>();
        rb2D = jefe.rb2D;
        jugador = jefe.jugador; // Asume que tienes una referencia al jugador en tu script Jefe


        jefe.MirarJugador();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rb2D.linearVelocity = new Vector2(velocidadMovimiento, rb2D.linearVelocity.y) * animator.transform.right;
        // Calcular dirección hacia el jugador
        Vector2 direccion = (jugador.position - animator.transform.position).normalized;

        // Aplicar velocidad en ambas direcciones
        rb2D.linearVelocity = direccion * velocidadMovimiento;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //rb2D.linearVelocity = new Vector2(0, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector2.zero; // Detener completamente el movimiento

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
