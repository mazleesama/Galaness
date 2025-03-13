using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassportUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text ageText;
    public Image avatarImage;
    public Image religionIcon;
    public Image sexualityIcon;
    public Image handicapIcon;
    public Button selectButton;


    private Passport currentPassport;
    private PassportManager passportManager;

    public void Setup(Passport passport, PassportManager manager)
    {
        currentPassport = passport;
        passportManager = manager;

        nameText.text = passport.name;
        ageText.text = "Age: " + passport.age;
        avatarImage.sprite = passport.avatar;

        handicapIcon.enabled = passport.hasHandicap;

        selectButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        passportManager.OpenZoomPanel(currentPassport);
    }
}