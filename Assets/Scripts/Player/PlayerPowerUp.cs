using System;
using UnityEngine;
using UnityEngine.UI;
using XpFacultad.JuegoPlataformasUnity2D.ScriptableObjects.Collectables;

namespace XpFacultad.JuegoPlataformasUnity2D.Player
{
    public class PlayerPowerUp : MonoBehaviour
    {

        [Header("UI Objects")]
        [SerializeField] GameObject circleObject;
        [SerializeField] Image circleFill;

        float internalTimer = 0f;
        int currentDuration = 0;
        CollectableSO currentPowerUp;

        bool HasPowerUp { get { return currentPowerUp != null; } }

        // Update is called once per frame
        void Update()
        {
            if (HasPowerUp)
            {
                if (internalTimer < currentDuration)
                {
                    internalTimer += Time.deltaTime;
                    circleFill.fillAmount = 1 - (internalTimer / currentDuration);
                }
                else
                {
                    ClearPowerUp();
                }
            }
        }

        internal void SetPowerUp(CollectableSO collectableSO)
        {
            currentPowerUp = collectableSO;


            if (collectableSO.HasTimeLimit)
            {
                currentDuration = collectableSO.duration;
                internalTimer = 0f;
                circleObject.SetActive(true);
                circleFill.color = collectableSO.displayColor;
                circleFill.fillAmount = 1;
            }
            else
            {
                collectableSO.PerformGameProperty();
                ClearPowerUp();
            }
        }

        void ClearPowerUp()
        {
            currentPowerUp = null;
            internalTimer = 0f;
            currentDuration= 0;
            circleObject.SetActive(false);
            circleFill.fillAmount = 1;
        }
    }
}
