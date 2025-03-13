using UnityEngine;

[System.Serializable]
public class Passport
{
    public string name;
    public int age;
    public string religion;
    public string sexuality;
    public bool hasHandicap;
    public Sprite avatar; // Pour stocker l’image aléatoire

    // Constructeur pour générer des valeurs aléatoires
    public Passport(Sprite randomAvatar)
    {
        string[] names = { "Alex", "Sam", "Jordan", "Charlie", "Taylor" };
        string[] religions = { "Religieux", "Athée" };
        string[] sexualities = { "Hétéro", "LGBT" };

        name = names[Random.Range(0, names.Length)];
        age = Random.Range(18, 70);
        religion = religions[Random.Range(0, religions.Length)];
        sexuality = sexualities[Random.Range(0, sexualities.Length)];
        hasHandicap = (Random.value > 0.7f);
        avatar = randomAvatar; // Avatar assigné lors de la création
    }
}
