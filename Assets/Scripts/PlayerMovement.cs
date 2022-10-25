using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float movementSpeed = 7f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] LayerMask jumpableGround;

    Rigidbody2D rigidbody2D;
    Animator animator;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    [Header("Sound Effects")]
    [SerializeField] AudioSource soundJump;


    private enum MovementState { idle, running, jumping, falling }

    bool IsGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float dirX = Input.GetAxis("Horizontal");
        float dirY = Input.GetAxis("Vertical");

        rigidbody2D.velocity = new Vector2(dirX * movementSpeed, rigidbody2D.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            soundJump.Play();
            rigidbody2D.velocity = new Vector3(0, jumpForce, 0);
        }

        UpdateMovementAnimation(dirX);
    }

    private void UpdateMovementAnimation(float dirX)
    {
        MovementState movementState = MovementState.idle;

        if (dirX > 0 || dirX < 0)
        {
            movementState = MovementState.running;
            spriteRenderer.flipX = dirX < 0 ? true : false;
        }
        else
        {
            movementState = MovementState.idle;
        }

        if (rigidbody2D.velocity.y > .1f)
        {
            movementState = MovementState.jumping;
        }
        else if (rigidbody2D.velocity.y < -.1f)
        {
            movementState= MovementState.falling;
        }

        animator.SetInteger("state", (int)movementState);
    }
}
