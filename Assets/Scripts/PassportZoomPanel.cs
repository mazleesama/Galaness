using UnityEngine;
using UnityEngine.UI;

public class PassportZoomPanel : MonoBehaviour
{
    public Text nameText;
    public Text ageText;
    public Image avatarImage;
    public Image religionIcon;
    public Image sexualityIcon;
    public Image handicapIcon;

    private Passport currentPassport;

    public void ShowPassport(Passport passport)
    {
        currentPassport = passport;

        nameText.text = passport.name;
        ageText.text = "Age: " + passport.age;
        avatarImage.sprite = passport.avatar;

        // Remplace ces ic�nes par tes propres sprites
        // religionIcon.sprite = ...
        // sexualityIcon.sprite = ...
        handicapIcon.enabled = passport.hasHandicap;

        gameObject.SetActive(true);
    }

    public void OnClickAccept()
    {
        GameManager.Instance.OnPassportAccepted(currentPassport);
        Debug.Log("Passeport accept� : " + currentPassport.name);
    }

    public void OnClickHold()
    {
        GameManager.Instance.OnPassportHold(currentPassport);
        Debug.Log("Passeport mis en attente : " + currentPassport.name);
    }
}
