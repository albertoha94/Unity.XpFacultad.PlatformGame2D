using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.Enemy;
using XpFacultad.JuegoPlataformasUnity2D.Player;

public class PlayerCombat : MonoBehaviour
{

    [SerializeField] PlayerHitboxManager hitboxManager;
    [SerializeField] float attackRatePerSecond = 0.5f;
    [SerializeField] int baseDamage = 1;
    public bool canAttack = true;
    float nextAttackTime = 0f;

    Animator animator;
    SpriteRenderer spriteRenderer;

    public bool CanAttack
    {
        get { return canAttack; }
        set { canAttack = value; }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        var deltatime = Time.deltaTime;
        if (nextAttackTime >= attackRatePerSecond)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                PerformAttackBase();
                nextAttackTime = 0f;
            }
        }
        else
        {
            nextAttackTime += deltatime;
        }
    }

    public void DoDamage(EnemyHealth enemyHealth)
    {
        enemyHealth.TakeDamage(baseDamage);
    }

    private void SetCombatHitbox(int index) => hitboxManager.SetCombatHitbox(index, spriteRenderer.flipX);

    private void PerformAttackBase()
    {
        if (canAttack)
        {
            animator.SetTrigger(PlayerParameters.ATTACK);
        }
    }
}
