using UnityEngine;

public class golpearma : MonoBehaviour
{
    [Header("Armas")]
    public Sprite armalvl0;
    public Sprite armalvl1;
    public Sprite armalvl2;
    public Sprite armalvl3;
    public Sprite armalvl4;
    public Sprite armalvl5;
    public Sprite armalvl6;
    public Sprite armalvl7;

    [Header("Sprite Renderer del arma")]
    public SpriteRenderer armaRenderer;

    [Header("Animator")]
    private Animator animator;

    public float cooldownTime = 1.0f;
    private float nextGolpeTime = 0f;

    private int lastLevel = -1;

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateWeaponSprite();
    }

    void Update()
    {
        if (ExperienceManager.CurrentLevel != lastLevel)
        {
            UpdateWeaponSprite();
        }

        if (Time.time >= nextGolpeTime)
        {
            animator.SetTrigger("Golpear");
            nextGolpeTime = Time.time + cooldownTime;
        }
    }

    void UpdateWeaponSprite()
    {
        lastLevel = ExperienceManager.CurrentLevel;

        Sprite nuevoSprite = armalvl0;

        switch (lastLevel)
        {
            case 0: nuevoSprite = armalvl0; break;
            case 1: nuevoSprite = armalvl1; break;
            case 2: nuevoSprite = armalvl2; break;
            case 3: nuevoSprite = armalvl3; break;
            case 4: nuevoSprite = armalvl4; break;
            case 5: nuevoSprite = armalvl5; break;
            case 6: nuevoSprite = armalvl6; break;
            case 7: nuevoSprite = armalvl7; break;
            default: nuevoSprite = armalvl7; break;
        }

        if (armaRenderer != null)
        {
            armaRenderer.sprite = nuevoSprite;
        }
    }
}
