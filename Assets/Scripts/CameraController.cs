using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] Transform playerT;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(playerT.position.x, playerT.position.y, transform.position.z);
    }
}
