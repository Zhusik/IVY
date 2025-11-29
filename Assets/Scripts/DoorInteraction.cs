using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public bool goNext = true;
    private bool playerIsNear = false;

    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;

            if (goNext)
            {
                SceneManager.LoadScene(currentIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(currentIndex - 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
            Debug.Log("Player near door");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
