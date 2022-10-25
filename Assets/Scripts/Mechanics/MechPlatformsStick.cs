using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechPlatformsStick : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collisionGo = collision.gameObject;
        if (collisionGo.CompareTag("Player"))
        {
            collisionGo.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var collisionGo = collision.gameObject;
        if (collisionGo.CompareTag("Player"))
        {
            collisionGo.transform.SetParent(null);
        }
    }
}
