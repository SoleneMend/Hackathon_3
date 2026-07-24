using UnityEngine;
using UnityEngine.UI;

public class CoinProgressBar : MonoBehaviour
{
    [Header("Références UI")]
    public Slider progressSlider; // glisse ton Slider ici dans l'Inspector

    [Header("Paramètres")]
    public int totalCoinsToCollect = 10;
    private int currentCoins = 0;

    [Header("Animation (optionnel)")]
    public float smoothSpeed = 5f;
    private float targetFillAmount = 0f;

    void Start()
    {
        if (progressSlider != null)
        {
            progressSlider.minValue = 0f;
            progressSlider.maxValue = 1f;
            progressSlider.value = 0f;
        }
    }

    void Update()
    {
        // Anime la barre en douceur vers la nouvelle valeur
        if (progressSlider != null)
        {
            progressSlider.value = Mathf.Lerp(
                progressSlider.value,
                targetFillAmount,
                Time.deltaTime * smoothSpeed
            );
        }
    }

    // Appelée à chaque fois qu'une pièce est ramassée
    public void CollectCoin()
    {
        currentCoins++;
        currentCoins = Mathf.Min(currentCoins, totalCoinsToCollect);
        targetFillAmount = (float)currentCoins / totalCoinsToCollect;

        if (currentCoins >= totalCoinsToCollect)
        {
            OnAllCoinsCollected();
        }
    }

    void OnAllCoinsCollected()
    {
        Debug.Log("Toutes les pièces ont été collectées !");
        // Ajoute ici ta logique de fin de niveau, victoire, etc.
    }
}