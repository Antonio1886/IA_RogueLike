using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    /*void Start()
    {
        slider = GetComponent<Slider>();
    }*/

    public void CambiarVidaMaxima(float vidaMaxima)
    {
        if (slider == null) slider = GetComponent<Slider>();
        slider.maxValue = vidaMaxima;
    }

    public void CambiarVidaActual(float cantidadVida)
    {
        slider.value = cantidadVida;
    }

    public void InicializarBarraDeVida(float cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }
}
