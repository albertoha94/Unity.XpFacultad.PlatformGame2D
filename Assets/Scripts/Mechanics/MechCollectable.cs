using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.ScriptableObjects.Collectables;

namespace XpFacultad.JuegoPlataformasUnity2D.Mechanics
{
    public class MechCollectable : MonoBehaviour
    {

        [SerializeField] CollectableSO collectable;

        public void Collect() => collectable.Collect();
    }
}