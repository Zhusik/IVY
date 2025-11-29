using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float moveSpeed = 3f;

    private bool isFacingRight = true;
    private bool canMove = true;
    private bool isDead = false;

    private const string IS_RUNNING = "IsRunning";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isFacingRight = Mathf.Sign(transform.localScale.x) > 0;
    }

    private void FixedUpdate()
    {
        if (isDead || !canMove) return;

        HandleMovement();
        UpdateAnimations();
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (moveX > 0 && !isFacingRight) Flip();
        if (moveX < 0 && isFacingRight) Flip();
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    private void UpdateAnimations()
    {
        bool walking = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        animator.SetBool(IS_RUNNING, walking);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        canMove = false;

        rb.linearVelocity = Vector2.zero;

        animator.SetTrigger("Die");
        animator.SetBool(IS_RUNNING, false);

        Debug.Log("Player died!");
    }

    public void DisableMovement()
    {
        if (!isDead)
        {
            canMove = false;
            animator.SetBool(IS_RUNNING, false);
        }
    }

    public void EnableMovement()
    {
        if (!isDead)
        {
            canMove = true;
        }
    }
}
