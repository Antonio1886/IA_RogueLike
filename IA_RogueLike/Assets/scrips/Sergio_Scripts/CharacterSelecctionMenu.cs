using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelecctionMenu : MonoBehaviour
{
    public GameObject knight;
    public GameObject Oni;
    public GameObject Lancer;
    public GameObject Rogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectKnight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectKnight()
    {
        SetAllInactive();
        knight.SetActive(true);
    }
    public void SelectOni()
    {
        SetAllInactive();
        Oni.SetActive(true);
    }
    public void SelectLancer()
    {
        SetAllInactive();
        Lancer.SetActive(true);
    }
    public void SelectRogue()
    {
        SetAllInactive();
        Rogue.SetActive(true);
    }

    private void SetAllInactive()
    {
        knight.SetActive(false);
        Oni.SetActive(false);
        Lancer.SetActive(false);
        Rogue.SetActive(false);
    }

    public void StartGame()
    {
        if (knight.activeSelf)
        {
            SceneManager.LoadScene("KnightScene");
        }
        else if (Oni.activeSelf)
        {
            SceneManager.LoadScene("OniScene");
        }
        else if (Lancer.activeSelf)
        {
            SceneManager.LoadScene("LancerScene");
        }
        else if (Rogue.activeSelf)
        {
            SceneManager.LoadScene("RogueScene");
        }
        else
        {
            Debug.LogWarning("No se ha seleccionado ningún personaje");
            // Opcional: Seleccionar uno por defecto o mostrar mensaje al usuario
        }
    }


}
