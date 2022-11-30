using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    public enum MovementState { idle, running, attack }
    [SerializeField] private MovementState movementState = MovementState.idle;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public MovementState EnemyState
    {
        get { return movementState; }
        set
        {
            movementState = value;
            animator.SetInteger("state", (int)movementState);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetToIdle() => EnemyState = MovementState.idle;

    public void SetToRunningRight() => SetToRunning();

    public void SetToRunningLeft() => SetToRunning(true);

    private void SetToRunning(bool flipX = false)
    {
        spriteRenderer.flipX = flipX;
        EnemyState = MovementState.running;
    }
}
