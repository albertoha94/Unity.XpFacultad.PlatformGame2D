using System.Collections;
using UnityEngine;

public class MechDestroyInSeconds : MonoBehaviour
{

    public int secondsToWait = 3;

    void Start()
    {
        StartCoroutine(WaitAndDie());
    }

    private IEnumerator WaitAndDie()
    {
        yield return new WaitForSeconds(secondsToWait);
        Destroy(gameObject);
    }
}
