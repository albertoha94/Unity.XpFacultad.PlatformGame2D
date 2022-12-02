using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using XpFacultad.JuegoPlataformasUnity2D.Common;

public class PlayerLifeComponent : MonoBehaviour
{

    [Header("Events")]
    [SerializeField] private UnityEvent onDie;

    Animator animator;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.DEATHTRAP))
        {
            OnDie();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ENEMY))
        {
            OnDie();
        }
    }

    private void OnDie()
    {
        onDie.Invoke();
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
