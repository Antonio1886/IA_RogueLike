using UnityEngine;

public class BlindnessEffect : MonoBehaviour
{
    [Header("Efecto Visual")]
    public CanvasGroup darknessOverlay;
    public float fadeSpeed = 3f;
    [Range(0, 1)] public float maxDarknessAlpha = 0.85f;

    [Header("Efecto de Sonido")]
    public AudioClip blindnessSound;
    public float effectVolume = 0.6f;
    private AudioSource audioSource;

    private float effectTimer = 0f;
    private bool isBlinded = false;
    private bool soundPlayed = false; // Para controlar que el sonido solo se reproduzca una vez

    void Start()
    {
        // Configurar AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;

        // Asegurar que el efecto no sea visible al inicio
        if (darknessOverlay != null)
        {
            darknessOverlay.alpha = 0f;
        }
    }

    void Update()
    {
        if (isBlinded)
        {
            effectTimer += Time.deltaTime;
            
            // Efecto visual con oscilación
            float baseAlpha = Mathf.PingPong(Time.time * 5f, 0.2f) + 0.6f;
            darknessOverlay.alpha = Mathf.Lerp(darknessOverlay.alpha, baseAlpha, Time.deltaTime * fadeSpeed);

            // Reproducir sonido solo al inicio del efecto
            if (!soundPlayed && blindnessSound != null)
            {
                audioSource.PlayOneShot(blindnessSound, effectVolume);
                soundPlayed = true;
            }
        }
        else
        {
            // Ocultar el oscurecimiento gradualmente
            darknessOverlay.alpha = Mathf.Lerp(darknessOverlay.alpha, 0f, Time.deltaTime * fadeSpeed);
            soundPlayed = false; // Resetear para la próxima vez
        }

        // Tecla de prueba (opcional, puedes quitarlo en la versión final)
        if (Input.GetKeyDown(KeyCode.T))
        {
            TriggerBlindness(3f);
        }
    }

    public void TriggerBlindness(float duration)
    {
        if (!isBlinded) // Solo activar si no está ya activo
        {
            isBlinded = true;
            effectTimer = 0f;
            soundPlayed = false; // Aseguramos que el sonido se reproduzca
            Invoke("EndBlindness", duration);
        }
    }

    private void EndBlindness()
    {
        isBlinded = false;
    }
}