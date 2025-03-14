using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PassportZoomPanel : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text ageText;
    public Image avatarImage;
    public Image religionIcon;
    public Image sexualityIcon;
    public Image handicapIcon;
    public Button acceptButton;
    public Button holdButton;

    private Passport currentPassport;

    public void ShowPassport(Passport passport)
    {
        if (passport == null)
        {
            Debug.LogWarning("⚠️ Passeport invalide !");
            return;
        }

        Debug.Log("✅ ShowPassport() appelé pour " + passport.name);

        currentPassport = passport;
        gameObject.SetActive(true); // ✅ Active le panel

        nameText.text = passport.name;
        ageText.text = "Age: " + passport.age;
        avatarImage.sprite = passport.avatar;
        handicapIcon.enabled = passport.hasHandicap;

        // ✅ Ajoute une animation pour forcer l'affichage
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic);

        Debug.Log("📜 Panel de zoom affiché avec animation");
    }

    public void OnClickAccept()
    {
        Debug.Log("✅ Passeport accepté : " + currentPassport.name);
        GameManager.Instance.OnPassportAccepted(currentPassport);
        gameObject.SetActive(false); // Ferme le zoom
    }

    public void OnClickHold()
    {
        Debug.Log("⏳ Passeport en attente : " + currentPassport.name);
        GameManager.Instance.OnPassportHold(currentPassport);
        gameObject.SetActive(false); // Ferme le zoom
    }
}