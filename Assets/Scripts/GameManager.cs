using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 

    [Header("Gestion du Jeu")]
    public float planetPopularity = 50f;
    public int happyCitizens = 0;

    [Header("Références UI")]
    public Slider popularitySlider;
    public Text happyCitizensText;

    [Header("Gestion des Passeports")]
    public PassportManager passportManager;
    public PassportZoomPanel zoomPanel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        UpdateUI();
    }

    public void OnPassportAccepted(Passport passport)
    {
        Debug.Log("✅ Passeport accepté : " + passport.name);

        happyCitizens++;
        planetPopularity = Mathf.Min(planetPopularity + 5f, 100f);
        UpdateUI();

        zoomPanel.gameObject.SetActive(false);

        // Vérification avant de générer un nouveau passeport
        if (passportManager.currentPassports.Count < 6) 
        {
            passportManager.GeneratePassports(1); // Utilisation de la bonne méthode
        }
    }

    public void OnPassportHold(Passport passport)
    {
        Debug.Log("⏳ Passeport mis en attente : " + passport.name);

        planetPopularity = Mathf.Max(planetPopularity - 3f, 0f);
        UpdateUI();

        zoomPanel.gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        popularitySlider.value = planetPopularity;
        happyCitizensText.text = "☻" + happyCitizens;

        if (planetPopularity <= 0)
        {
            Debug.Log("💀 Game Over : la planète a perdu toute sa popularité !");
        }
    }
}
