using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Combat")]
    public Transform firePoint;
    public float attackRange = 8f;
    public float fireRate = 2f;
    public float damage = 10f;
    public float projectileSpeed = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Debug.Log(animator.gameObject.name);

        currentHealth = maxHealth;
    }

    void Update()
    {
        HandleMovement();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }


    void HandleMovement()
    {
        input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed)
                input.y += 1;

            if (Keyboard.current.sKey.isPressed)
                input.y -= 1;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1;

            if (Keyboard.current.aKey.isPressed)
                input.x -= 1;
        }

        input.Normalize();

        if(animator != null)
            animator.SetFloat("Speed", input.magnitude);

        if(input.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(input.x), 1, 1);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if(currentHealth <= 0)
            Die();
    }

    public void Heals(int hp_points)
    {
        if ((currentHealth + hp_points) >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += hp_points;
        }
    }

    void Die()
    {
        Debug.Log("Player is dead!");
        FindAnyObjectByType<GameOverMenu>().GameOver();

    }
}