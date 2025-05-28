using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private Image vidaImageUI;

    private TopDownMovement movementScript; // Referencia al script de movimiento

    void Start()
    {
        vida = vidaMaxima;
        movementScript = GetComponent<TopDownMovement>(); // Obtener la referencia

        if (vidaImageUI != null)
        {
            vidaImageUI.type = Image.Type.Filled;
            vidaImageUI.fillMethod = Image.FillMethod.Horizontal;
            vidaImageUI.fillAmount = vida / vidaMaxima;
        }
    }

    public void TomarDaño(float daño)
    {
        // Verificar si el jugador está haciendo dash
        if (movementScript != null && movementScript.IsDashing())
        {
            return; // No recibir daño si está en dash
        }

        vida -= daño;
        ActualizarVidaUI();

        if (vida <= 0)
        {
            vida = 0;
        }
    }

    public void Curarse(float cura)
    {
        vida += cura;
        ActualizarVidaUI();

        if (vida > vidaMaxima)
        {
            vida = vidaMaxima;
        }
    }

    private void ActualizarVidaUI()
    {
        if (vidaImageUI != null)
        {
            vidaImageUI.fillAmount = Mathf.Clamp01(vida / vidaMaxima);
        }
    }

    public float GetCurrentHealth()
    {
        return vida;
    }
}