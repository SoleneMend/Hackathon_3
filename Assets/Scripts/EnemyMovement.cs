using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float vitesse = 2f;
    public int degats = 10;

    private Transform joueur;
    private Rigidbody2D rb;

    void Start()
    {
        // Trouve automatiquement le joueur via son tag
        joueur = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (joueur == null) return;

        Vector2 direction = (joueur.position - transform.position).normalized;
        rb.linearVelocity = direction * vitesse;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth vie = collision.gameObject.GetComponent<PlayerHealth>();
            if (vie != null)
                vie.SubirDegats(degats);
        }
    }
}