using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float moveSpeed = 3f;

    private bool isFacingRight = true;
    private bool canMove = true;
    private bool isDead = false;

    private const string IS_WALKING = "IsRunning";
    private const string IS_DEAD = "IsDead";

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
        if (!canMove || isDead) return;

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
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void UpdateAnimations()
    {
        bool isWalking = Mathf.Abs(rb.linearVelocity.x) > 0.1f && canMove && !isDead;
        animator.SetBool(IS_WALKING, isWalking);
        animator.SetBool(IS_DEAD, isDead);
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        canMove = false;
        rb.linearVelocity = Vector2.zero;

        animator.SetBool(IS_DEAD, true);
        animator.SetBool(IS_WALKING, false);

        Debug.Log("Player died!");

        // Invoke("ReloadScene", 2f);
    }

    public void DisableMovement()
    {
        if (isDead) return;
        canMove = false;
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        animator.SetBool(IS_WALKING, false);
    }

    public void EnableMovement()
    {
        if (isDead) return;
        canMove = true;
    }

    private void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}