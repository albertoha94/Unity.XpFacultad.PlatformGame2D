using UnityEngine;

namespace XpFacultad.JuegoPlataformasUnity2D.Player
{
    public class PlayerHitboxManager : MonoBehaviour
    {

        [SerializeField] Transform playerTransform;
        [SerializeField] PolygonCollider2D[] combatHitboxes;

        private void Update()
        {
            transform.position = playerTransform.position;
        }

        public void SetCombatHitbox(int index, bool flipX)
        {
            for (int i = 0; i < combatHitboxes.Length; i++)
            {
                var currentHitbox = combatHitboxes[i];
                var newLocalScale = currentHitbox.transform.localScale;
                newLocalScale.x = flipX ? newLocalScale.x * -1 : newLocalScale.x;
                currentHitbox.transform.localScale = newLocalScale;
                currentHitbox.gameObject.SetActive(i == index);
            }
        }
    }
}
