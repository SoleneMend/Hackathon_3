using UnityEngine;

public class Coin : MonoBehaviour
{
    // Pour un jeu 2D. Pour un jeu 3D, remplace par OnTriggerEnter(Collider other)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CoinProgressBar progressBar = FindObjectOfType<CoinProgressBar>();
            if (progressBar != null)
            {
                progressBar.CollectCoin();
            }

            Destroy(gameObject);
        }
    }
}