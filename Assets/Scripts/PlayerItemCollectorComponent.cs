using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItemCollectorComponent : MonoBehaviour
{

    [SerializeField] TMP_Text cherriesText;
    int cherries = 0;

    [Header("Sound Effects")]
    [SerializeField] AudioSource soundScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text= "Cherries:" + cherries;
            soundScore.Play();
        }
    }
}
