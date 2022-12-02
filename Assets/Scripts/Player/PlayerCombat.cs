using System;
using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.Enemy;

namespace XpFacultad.JuegoPlataformasUnity2D.Player
{
    public class PlayerCombat : MonoBehaviour
    {

        [Header("Attack Speed")]
        [SerializeField] int baseDamage = 1;
        [SerializeField] float attackRatePerSecond = 0.5f;
        [SerializeField] PolygonCollider2D[] combatHitboxes;
        public bool canAttack = true;
        float nextAttackTime = 0f;

        Animator animator;

        public bool CanAttack
        {
            get { return canAttack; }
            set { canAttack = value; }
        }

        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();
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

        private void SetCombatHitbox(int index)
        {
            for (int i = 0; i < combatHitboxes.Length; i++)
            {
                if (i == index)
                {
                    combatHitboxes[i].gameObject.SetActive(true);
                }
                else
                {
                    combatHitboxes[i].gameObject.SetActive(false);
                }
            }
        }

        private void PerformAttackBase()
        {
            if (canAttack)
            {
                animator.SetTrigger(PlayerParameters.ATTACK);
            }
        }
    }
}
