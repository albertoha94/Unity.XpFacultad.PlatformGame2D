using TMPro;
using UnityEngine;
using XpFacultad.JuegoPlataformasUnity2D.Mechanics;

public class PlayerItemCollectorComponent : MonoBehaviour
{

    [SerializeField] TMP_Text cherriesText;
    int cherries = 0;

    [Header("Sound Effects")]
    [SerializeField] AudioSource soundScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        var go = collision.gameObject;
        if (go.CompareTag("Collectable"))
        {
            soundScore.Play();
            cherries++;
            cherriesText.text= "Cherries:" + cherries;
            if (go.TryGetComponent(out MechCollectable mechCollectable))
            {
                mechCollectable.Collect();
            }
            Destroy(go);
        }
    }
}
