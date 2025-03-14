using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Assurez-vous d'importer DOTween pour les animations

public class PassportManager : MonoBehaviour
{
    public GameObject passportPrefab; // Prefab du passeport
    public Transform passportContainer; // Conteneur (GridLayoutGroup)
    public PassportZoomPanel zoomPanel; // Référence vers le Panel de Zoom

    public List<Sprite> avatarSprites; // Liste des avatars
    public List<Passport> currentPassports = new List<Passport>();

    private int maxPassports = 9; // 3x3

    void Start()
    {
        GeneratePassports(maxPassports);
    }

    public void GeneratePassports(int count)
    {
        StartCoroutine(GeneratePassportsWithAnimation(count));
    }

    private IEnumerator GeneratePassportsWithAnimation(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (currentPassports.Count >= maxPassports) yield break;

            if (avatarSprites.Count == 0)
            {
                Debug.LogWarning("⚠️ Aucune image d'avatar disponible !");
                yield break;
            }

            Sprite randomAvatar = avatarSprites[Random.Range(0, avatarSprites.Count)];
            Passport newPassport = new Passport(randomAvatar);
            currentPassports.Add(newPassport);

            GameObject newPassportUI = Instantiate(passportPrefab, passportContainer);
            PassportUI passportUIScript = newPassportUI.GetComponent<PassportUI>();
            passportUIScript.Setup(newPassport, this);

            // ✅ Corrige la taille immédiatement après l’instanciation
            RectTransform rectTransform = newPassportUI.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(175, 100); // Fixe la taille

            // ✅ Corrige l’animation pour ne pas rétrécir le passeport
            newPassportUI.transform.localScale = Vector3.zero;
            newPassportUI.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(0.1f);
        }
    }
    public void OpenZoomPanel(Passport passport)
    {
        if (passport == null)
        {
            Debug.LogWarning("⚠️ Passeport invalide !");
            return;
        }

        Debug.Log("🔍 Zoom sur : " + passport.name);

        if (zoomPanel == null)
        {
            Debug.LogError("❌ ERREUR : `zoomPanel` est null !");
            return;
        }

        zoomPanel.ShowPassport(passport);
    }
}
