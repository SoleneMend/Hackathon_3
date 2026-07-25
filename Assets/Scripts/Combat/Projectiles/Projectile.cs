using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Stats reçues du Player")]
    public float speed;
    public float damage;

    public float lifeTime = 3f;

    private Vector2 direction;


    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    public void SetStats(float projectileSpeed, float projectileDamage)
    {
        speed = projectileSpeed;
        damage = projectileDamage;
    }


    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0f, 0f, angle);
    }


    void Update()
    {
        transform.Translate(
            direction * speed * Time.deltaTime,
            Space.World
        );
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}