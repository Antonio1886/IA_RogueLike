using UnityEngine;

public class GameManager : MonoBehaviour
{    public static GameManager Instance;
    public GameObject hitCanvas;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowHitCanvas()
    {
        if (hitCanvas != null)
            hitCanvas.SetActive(true);
    }
}
