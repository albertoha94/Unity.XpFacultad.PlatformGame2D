using System;
using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.Common;
using XpFacultad.JuegoPlataformasUnity2D.Player;

namespace XpFacultad.JuegoPlataformasUnity2D.ScriptableObjects.Collectables
{

    [CreateAssetMenu(fileName = "Collectable", menuName = "ScriptableObjects/Collectable", order = 1)]
    public class CollectableSO : ScriptableObject
    {
        [SerializeField] public int duration = -1;
        [SerializeField] public Color displayColor = Color.white;
        [SerializeField] GameProperty collectableProperty;
        [SerializeField] float collectableValue;

        public enum GameProperty { ScoreIncreaseBy, AttackIncreaseBy };

        public bool HasTimeLimit => duration > 0;

        public void Collect()
        {
            var player = GameObject.FindWithTag(Tags.PLAYER);
            if (player != null)
            {
                if (player.TryGetComponent(out PlayerPowerUp playerPowerUp))
                {
                    playerPowerUp.SetPowerUp(this);
                }
            }
        }

        public void PerformGameProperty()
        {
            switch (collectableProperty)
            {
                case GameProperty.ScoreIncreaseBy:
                    break;
                case GameProperty.AttackIncreaseBy:
                    break;
            }
        }
    }
}
