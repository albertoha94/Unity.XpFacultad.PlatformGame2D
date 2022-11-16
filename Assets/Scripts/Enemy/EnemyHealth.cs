using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.Common;
using XpFacultad.JuegoPlataformasUnity2D.Player;

namespace XpFacultad.JuegoPlataformasUnity2D.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] GameObject destructionFX;
        [SerializeField] Transform destructionTransform;
        [SerializeField] GameObject objectToDestroyOnDeath;
        [SerializeField] int health = 1;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(Tags.PLAYER_DAMAGE_HITBOX))
            {
                if (collision.gameObject.TryGetComponent(out PlayerCombatHitbox playerCombatHitbox))
                {
                    TakeDamage(playerCombatHitbox.baseDamage);
                }
            }
        }

        public void TakeDamage(int amount)
        {
            health -= amount;
            if (health <= 0)
            {
                OnDie();
            }
            else
            {

            }
        }

        private void OnDie()
        {
            Instantiate(destructionFX, destructionTransform.position, destructionTransform.rotation);
            Destroy(objectToDestroyOnDeath);
        }
    }
}
