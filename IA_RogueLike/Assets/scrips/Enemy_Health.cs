using UnityEngine;
using System.Collections;

public class Enemy_Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int experienceReward;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        // Buscar el ExperienceManager y agregar experiencia
        ExperienceManager experienceManager = FindObjectOfType<ExperienceManager>();
        if (experienceManager != null)
        {
            experienceManager.AddExperience(experienceReward);
        }

        Destroy(gameObject);
    }
}