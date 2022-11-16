using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.Player;

namespace XpFacultad.JuegoPlataformasUnity2D.Player
{
    public class PlayerCombat : MonoBehaviour
    {

        [Header("Attack Speed")]
        [SerializeField] float attackRatePerSecond = 0.5f;
        float nextAttackTime = 0f;

        Animator animator;
        PlayerMovement playerMovement;

        // Start is called before the first frame update
        void Awake()
        {
            animator = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
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

        private void PerformAttackBase()
        {
            animator.SetTrigger(PlayerParameters.ATTACK);
        }
    }
}
