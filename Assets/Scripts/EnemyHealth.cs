using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 10;

    [Header("Loot")]
    [SerializeField] private GameObject goldPrefab;
    [Range(0f, 1f)]
    [SerializeField] private float dropChance = 0.3f; // 30% de chance

    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

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