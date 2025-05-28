using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Enemy enemy;

    public Transform target;

    [Header("Parámetros de comportamiento")]
    public float detectionRange = 8f;
    public float attackRange = 1.5f;         // 👈 Ahora pública y visible
    public float attackCooldown = 2f;
    public float moveSpeed = 2f;
    public float retreatDistance = 1f;

    private enum State { Idle, Chasing, Attacking, Retreating }
    private State currentState = State.Idle;

    private Vector3 retreatDirection;
    private float retreatTime;
    private float retreatDuration = 1f;

    void Start()
    {
        enemy = new Enemy(100, 10, attackRange, attackCooldown);
            // Buscar al jugador de forma más segura
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


    void Update()
    {
        if (!enemy.IsAlive()) return;

        float distance = Vector3.Distance(transform.position, target.position);
        float currentTime = Time.time;

        switch (currentState)
        {
            case State.Idle:
                if (distance <= detectionRange)
                    currentState = State.Chasing;
                break;

            case State.Chasing:
                if (distance > attackRange)
                {
                    MoveTowards(target.position);
                }
                else if (enemy.TryAttack(currentTime))
                {

                    Debug.Log("Ataque realizado. Daño: " + enemy.AttackDamage);
                    // Aquí puedes aplicar daño al jugador con un sistema de salud

                    retreatDirection = (transform.position - target.position).normalized;
                    retreatTime = Time.time;
                    currentState = State.Retreating;
                }
                break;

            case State.Retreating:
                if (Time.time - retreatTime < retreatDuration)
                {
                    MoveTowards(transform.position + retreatDirection * retreatDistance);
                }
                else
                {
                    currentState = State.Chasing;
                }
                break;
        }
    }

    private void MoveTowards(Vector3 destination)
    {
        Vector3 direction = (destination - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
