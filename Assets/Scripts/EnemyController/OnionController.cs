using UnityEngine;
using System.Collections;

public class OnionController : MonoBehaviour
{
    [SerializeField] float watchTime = 3f;
    [SerializeField] float restTime = 5f;
    [SerializeField] float targetHeight = 4.2f;

    private Animator anim;
    private bool isActive = true;
    private bool isWatching = false;

    private void Start()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = targetHeight;
        transform.position = newPosition;

        anim = GetComponent<Animator>();
        StartCoroutine(WatchCycle());
    }

    private IEnumerator WatchCycle()
    {
        while (isActive)
        {
            isWatching = false;
            anim.SetBool("isWatching", false);
            yield return new WaitForSeconds(restTime);

            isWatching = true;
            anim.SetBool("isWatching", true);
            yield return new WaitForSeconds(watchTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isWatching)
        {
            Debug.Log("Player detected! Destroying player...");
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isWatching)
        {
            Debug.Log("Player detected! Destroying player...");
            Destroy(collision.gameObject);
        }
    }
}