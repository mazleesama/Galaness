using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Assurez-vous d'importer DOTween pour les animations

public class PassportManager : MonoBehaviour
{
    public GameObject passportPrefab; // Prefab du passeport
    public Transform passportContainer; // Conteneur (GridLayoutGroup)
    public PassportZoomPanel zoomPanel; // Panel de zoom

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

            // ✅ Correction : Réduction de la taille dès le début
            //newPassportUI.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // Taille réduite à 50%

            // ✅ Animation fluide d'apparition sans agrandir trop
            newPassportUI.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.5f).SetEase(Ease.OutBack);

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

        zoomPanel.ShowPassport(passport);
    }
}
