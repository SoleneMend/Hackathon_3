using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float speed = 2f;
    public int damage = 10;
    public float maxHealth = 10f;

    [Header("Attaque")]
    public float attackDistance = 1f;
    public float attackCooldown = 1.5f;

    [Header("Loot")]
    public GameObject goldPrefab;
    [Range(0f, 1f)]
    public float dropChance = 0.3f;

    [Header("Evitement obstacles")]
    public string obstacleTag = "Obstacle";
    public float detectionDistance = 1.5f;
    public float angleStep = 15f;
    public float maxAngle = 90f;


    private float currentHealth;
    private float attackTimer;

    private Transform player;
    private Player playerScript;

    private Rigidbody2D rb;
    private Animator animator;


    void Awake()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
            playerScript = obj.GetComponent<Player>();
        }
    }


    void FixedUpdate()
    {
        if (player == null)
            return;


        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackDistance / 4f)
        {
            rb.linearVelocity = Vector2.zero;
            Attack();
        }
        else
        {
            Vector2 desiredDirection =
                (player.position - transform.position).normalized;

            Vector2 finalDirection =
                FindFreeDirection(desiredDirection);

            rb.linearVelocity = finalDirection * speed;

            attackTimer = 0;
        }


        if(animator != null)
        {
            animator.SetFloat(
                "Speed",
                rb.linearVelocity.magnitude
            );
        }
    }


    void Attack()
    {
        attackTimer -= Time.fixedDeltaTime;

        if(attackTimer <= 0)
        {
            attackTimer = attackCooldown;


            if(animator != null)
                animator.SetTrigger("Attack");


            if(playerScript != null)
                playerScript.TakeDamage(damage);
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
            Die();
    }


    void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }


    void DropLoot()
    {
        if(goldPrefab == null)
            return;

        if(Random.value <= dropChance)
        {
            Instantiate(
                goldPrefab,
                transform.position,
                Quaternion.identity
            );
        }
    }


    Vector2 FindFreeDirection(Vector2 direction)
    {
        if(!IsObstacle(direction))
            return direction;


        for(float angle = angleStep; angle <= maxAngle; angle += angleStep)
        {
            Vector2 right = Rotate(direction, -angle);

            if(!IsObstacle(right))
                return right;


            Vector2 left = Rotate(direction, angle);

            if(!IsObstacle(left))
                return left;
        }


        return Vector2.zero;
    }


    bool IsObstacle(Vector2 direction)
    {
        RaycastHit2D[] hits =
            Physics2D.RaycastAll(
                transform.position,
                direction,
                detectionDistance
            );


        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.CompareTag(obstacleTag))
                return true;
        }


        return false;
    }


    Vector2 Rotate(Vector2 vector, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }
}