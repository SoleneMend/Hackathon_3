using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Loot")]
    [SerializeField] private GameObject goldPrefab;
    [Range(0f, 1f)]
    [SerializeField] private float dropChance = 0.3f; // 30% de chance

    private int currentHealth = 10;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        TryDropLoot();
        Destroy(gameObject);
    }

    private void TryDropLoot()
    {
        if (goldPrefab == null) return;

        if (Random.value <= dropChance)
        {
            Instantiate(goldPrefab, transform.position, Quaternion.identity);
        }
    }
}