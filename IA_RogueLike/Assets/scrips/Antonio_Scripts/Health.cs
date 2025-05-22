using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraDeVida;

    void Start()
    {
        barraDeVida.InicializarBarraDeVida(vida);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        barraDeVida.CambiarVidaActual(vida);


        if (vida <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Curarse(float cura)
    {
        vida += cura;
        barraDeVida.CambiarVidaActual(vida);

        if (vida > 100)
        {
            vida = 100;
        }
    }
}
