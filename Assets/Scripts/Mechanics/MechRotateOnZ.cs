using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlatformGame.Mechanics
{

    public class MechRotateOnZ : MonoBehaviour
    {

        [SerializeField] float rotationSpeed = 2f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, 360 * rotationSpeed * Time.deltaTime);
        }
    }
}