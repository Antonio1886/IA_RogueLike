using UnityEngine;

public class Player_Weapon : MonoBehaviour
{
    public int damage = 10; // Cantidad de daño que hace esta arma

    private int lastLevel = -1;

    [Header("Dañospornivel")]
    public int damageLevel0 = 10;
    public int damageLevel1 = 15;
    public int damageLevel2 = 20;
    public int damageLevel3 = 25;
    public int damageLevel4 = 30;
    public int damageLevel5 = 35;
    public int damageLevel6 = 40;
    public int damageLevel7 = 45;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar si el objeto con el que colisionamos tiene el componente Enemy_Health
        Enemy_Health enemyHealth = collision.GetComponent<Enemy_Health>();

        if (enemyHealth != null)
        {
            // Si es un enemigo, le aplicamos el daño
            enemyHealth.TakeDamage(damage);
        }
    }

    private void Update()
    {
        if (ExperienceManager.CurrentLevel != lastLevel)
        {
            UpdateWeaponDamage();
        }

    }
    void UpdateWeaponDamage()
    {
        lastLevel = ExperienceManager.CurrentLevel;

        switch (lastLevel)
        {
            case 0: damage = damageLevel0; break;
            case 1: damage = damageLevel1; break;
            case 2: damage = damageLevel2; break;
            case 3: damage = damageLevel3; break;
            case 4: damage = damageLevel4; break;
            case 5: damage = damageLevel5; break;
            case 6: damage = damageLevel6; break;
            case 7: damage = damageLevel7; break;
            default: damage = damageLevel7; break;
        }

    }
}