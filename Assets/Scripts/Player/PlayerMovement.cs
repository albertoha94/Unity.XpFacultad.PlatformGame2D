using System;
using UnityEditor;
using UnityEngine;

namespace XpFacultad.JuegoPlataformasUnity2D.Player
{
    public class PlayerMovement : MonoBehaviour
    {

        [Header("Movement")]
        [SerializeField] bool canFlipX = true;
        [SerializeField] bool canMoveHorizontally = true;
        [SerializeField] float boxColliderVerticalTolerance = 0.1f;
        [SerializeField] float movementSpeed = 7f;
        [SerializeField] float jumpForce = 8f;
        [SerializeField] float fallMultiplier = 2.5f;
        [SerializeField] LayerMask groundLayerMask;

        Rigidbody2D rigidbody2D;
        Animator animator;
        SpriteRenderer spriteRenderer;
        BoxCollider2D boxCollider;

        [Header("Sound Effects")]
        [SerializeField] AudioSource soundJump;

        private enum MovementState { idle, running, jumping, falling }

        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            boxCollider = GetComponent<BoxCollider2D>();
        }

        // Update is called once per frame
        private void Update()
        {

            float dirX = Input.GetAxis("Horizontal");
            float dirY = Input.GetAxis("Vertical");

            rigidbody2D.velocity = new Vector2(
                canMoveHorizontally ? dirX * movementSpeed : 0f,
                rigidbody2D.velocity.y
                );
            Debug.Log("Velocity X:" + rigidbody2D.velocity.x);
            Debug.Log("Velocity Y:" + rigidbody2D.velocity.y);

            var isGrounded = IsGrounded();
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                soundJump.Play();
                rigidbody2D.velocity += Vector2.up * (jumpForce - 1);
            }

            if (rigidbody2D.velocity.y < 0.1f && !isGrounded)
            {
                rigidbody2D.velocity += Vector2.up * (fallMultiplier - 1) * Physics2D.gravity.y * Time.deltaTime;
            }

            UpdateMovementAnimation(dirX);

            #region Debug Code
            Color rayColor;
            if (isGrounded) rayColor = Color.yellow; else rayColor = Color.red;

            var boxColliderCenter = boxCollider.bounds.center;
            var boxColliderExtents = boxCollider.bounds.extents;
            Debug.DrawRay(boxColliderCenter + new Vector3(boxColliderExtents.x, 0), Vector3.down * (boxColliderExtents.y + boxColliderVerticalTolerance), rayColor);
            Debug.DrawRay(boxColliderCenter - new Vector3(boxColliderExtents.x, 0), Vector3.down * (boxColliderExtents.y + boxColliderVerticalTolerance), rayColor);
            Debug.DrawRay(boxColliderCenter - new Vector3(boxColliderExtents.x, boxColliderExtents.y + boxColliderVerticalTolerance), Vector2.right * boxCollider.size.x, rayColor);

            #endregion
        }

        public void DisableMovementBeforeAttack()
        {
            if (IsGrounded()) canMoveHorizontally = false;
            canFlipX = false;
        }

        internal bool IsGrounded()
        {
            var raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, boxColliderVerticalTolerance, groundLayerMask);
            return raycastHit.collider != null;
        }

        internal void StopHorizontalMovement()
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
        }

        private void UpdateMovementAnimation(float dirX)
        {
            MovementState movementState = MovementState.idle;

            if (dirX > 0 || dirX < 0)
            {
                movementState = MovementState.running;
                if (canFlipX)
                {
                    spriteRenderer.flipX = dirX < 0 ? true : false;
                }
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

        void EnableMovementAfterAttack()
        {
            canMoveHorizontally = true;
            canFlipX = true;
        }

        void TestFunction(bool x)
        {

        }
    }
}
