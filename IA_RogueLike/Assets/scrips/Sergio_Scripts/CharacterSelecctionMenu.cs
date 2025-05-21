using UnityEngine;

public class CharacterSelecctionMenu : MonoBehaviour
{
    public GameObject knight;
    public GameObject Oni;
    public GameObject Lancer;
    public GameObject Rogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectKnight()
    {
        knight.SetActive(true);
        Oni.SetActive(false);
        Lancer.SetActive(false);
        Rogue.SetActive(false);
    }
    public void SelectOni()
    {
        knight.SetActive(false);
        Oni.SetActive(true);
        Lancer.SetActive(false);
        Rogue.SetActive(false);
    }
    public void SelectLancer()
    {
        knight.SetActive(false);
        Oni.SetActive(false);
        Lancer.SetActive(true);
        Rogue.SetActive(false);
    }
    public void SelectRogue()
    {
        knight.SetActive(false);
        Oni.SetActive(false);
        Lancer.SetActive(false);
        Rogue.SetActive(true);
    }
}
