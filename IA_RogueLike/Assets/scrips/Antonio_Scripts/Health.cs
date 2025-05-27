using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private Image vidaImageUI; // Referencia a la imagen UI de vida

    void Start()
    {
        // Inicializar la vida al máximo
        vida = vidaMaxima;

        // Configurar la imagen de vida si está asignada
        if (vidaImageUI != null)
        {
            vidaImageUI.type = Image.Type.Filled;
            vidaImageUI.fillMethod = Image.FillMethod.Horizontal; // O el método que prefieras
            vidaImageUI.fillAmount = vida / vidaMaxima;
        }
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        ActualizarVidaUI();

        if (vida <= 0)
        {
            vida = 0;
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);
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
            // Asegurarse de que el valor esté entre 0 y 1
            vidaImageUI.fillAmount = Mathf.Clamp01(vida / vidaMaxima);
        }
    }
}