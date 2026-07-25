using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private float magnetSpeed = 2.5f;
    [SerializeField] private float magnetRange = 0.5f;
    [SerializeField] private float distanceRamassage = 0.15f;

    private bool ramassee;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

private void Update()
{
    if (player == null || ramassee) return;

    float distance = Vector2.Distance(transform.position, player.position);

    if (distance < magnetRange)
    {
        transform.position = Vector2.MoveTowards(
            transform.position, player.position, magnetSpeed * Time.deltaTime);
    }

    if (distance <= distanceRamassage)
{
    ramassee = true;
    if (GoldManager.Instance != null)
        GoldManager.Instance.AddGold(value);
    Destroy(gameObject);
}
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GoldManager.Instance.AddGold(value);
        Destroy(gameObject);
    }
}