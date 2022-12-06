using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlatformGame.Mechanics
{
    public class MechCompleteLevel : MonoBehaviour
    {

        [SerializeField] AudioSource finishSound;
        bool levelCompleted = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !levelCompleted)
            {
                finishSound.Play();
                levelCompleted = true;
                Invoke("CompleteLevel", 3f);
            }
        }

        private void CompleteLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
