using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float vitesse = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 input;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        input = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) input.y -= 1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) input.x += 1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) input.x -= 1;
        }
        input.Normalize();

        // Envoie la vitesse à l'Animator
        animator.SetFloat("vitesse", input.magnitude);

        // Flip du sprite selon la direction (gauche/droite)
        if (input.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(input.x), 1, 1);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * vitesse;
    }
}