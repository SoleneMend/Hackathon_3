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

        // Abonnement à l'event du GoldManager : la barre est notifiée
        // automatiquement à chaque changement de CurrentGold
        if (GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged += HandleGoldChanged;

            // Applique l'état actuel au cas où du gold aurait déjà été ramassé
            HandleGoldChanged(GoldManager.Instance.CurrentGold);
        }
        else
        {
            Debug.LogWarning("CoinProgressBar : aucun GoldManager trouvé dans la scène.");
        }
    }

    void OnDestroy()
    {
        // Se désabonner pour éviter les erreurs si cet objet est détruit avant le GoldManager
        if (GoldManager.Instance != null)
        {
            GoldManager.Instance.OnGoldChanged -= HandleGoldChanged;
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

    // Appelée automatiquement à chaque fois que CurrentGold change
    private void HandleGoldChanged(int newGoldTotal)
    {
        currentCoins = Mathf.Min(newGoldTotal, totalCoinsToCollect);
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