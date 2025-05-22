using UnityEngine;

public class PlayerHealt : MonoBehaviour
{
 public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Método para recibir daño
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} recibió {amount} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí puedes desactivar, destruir o hacer respawn del objeto
        Destroy(gameObject);
    }
}
