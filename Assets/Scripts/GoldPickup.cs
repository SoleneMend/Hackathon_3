using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    [SerializeField] private int value = 1;
    [SerializeField] private float magnetSpeed = 8f;
    [SerializeField] private float magnetRange = 2f;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) < magnetRange)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, player.position, magnetSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GoldManager.Instance.AddGold(value);
        Destroy(gameObject);
    }
}