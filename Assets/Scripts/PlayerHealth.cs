using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int vieMax = 100;
    public int vieActuelle;

    void Start()
    {
        vieActuelle = vieMax;
    }

    public void SubirDegats(int degats)
    {
        vieActuelle -= degats;
        vieActuelle = Mathf.Max(vieActuelle, 0); // évite les valeurs négatives

        Debug.Log("Vie restante : " + vieActuelle);

        if (vieActuelle <= 0)
        {
            Mourir();
        }
    }

    void Mourir()
    {
        Debug.Log("Le joueur est mort !");
        // Tu pourras ajouter ici : animation de mort, écran Game Over, désactivation du perso, etc.
        // Par exemple : gameObject.SetActive(false);
    }
}