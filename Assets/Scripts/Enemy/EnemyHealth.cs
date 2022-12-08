using UnityEngine;
using UnityEngine.Events;
using XpFacultad.JuegoPlataformasUnity2D.Common;
using XpFacultad.JuegoPlataformasUnity2D.Player;

namespace XpFacultad.JuegoPlataformasUnity2D.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {

        [Header("Properties")]
        [SerializeField] GameObject damageFX;
        [SerializeField] GameObject destructionFX;
        [SerializeField] Transform destructionTransform;
        [SerializeField] GameObject objectToDestroyOnDeath;
        [SerializeField] float canTakeDamageTimeThreshold = 0.5f;
        [SerializeField] int health = 1;

        float nextTakeDamageTime = 0f;
        bool canTakeDamage = true;

        [Header("Events")]
        [SerializeField] private UnityEvent onDamageTaken;

        void Update()
        {
            if (!canTakeDamage)
            {
                var deltatime = Time.deltaTime;
                if (nextTakeDamageTime >= canTakeDamageTimeThreshold)
                {
                    nextTakeDamageTime = 0f;
                    canTakeDamage = true;
                }
                else
                {
                    nextTakeDamageTime += deltatime;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(Tags.PLAYER_DAMAGE_HITBOX))
            {
                if (collision.gameObject.TryGetComponent(out PlayerHitbox playerCombatHitbox) && canTakeDamage)
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
                canTakeDamage = false;
                onDamageTaken.Invoke();
                Instantiate(damageFX, destructionTransform.position, destructionTransform.rotation);
            }
        }

        private void OnDie()
        {
            Instantiate(destructionFX, destructionTransform.position, destructionTransform.rotation);
            Destroy(objectToDestroyOnDeath);
        }
    }
}
