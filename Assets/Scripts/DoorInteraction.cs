using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorInteraction : MonoBehaviour
{
    public bool goNext = true;
    private bool playerIsNear = false;
    private bool playerIsStanding = false;
    private Animator playerAnimator;
    private float standingTime = 0f;
    private Vector3 lastPlayerPosition;
    private bool animationStarted = false;

    void Update()
    {
        if (playerIsNear && playerAnimator != null)
        {
            // Проверяем, стоит ли игрок на месте
            Vector3 currentPlayerPosition = playerAnimator.transform.position;
            float distanceMoved = Vector3.Distance(lastPlayerPosition, currentPlayerPosition);

            if (distanceMoved < 0.01f) // Если игрок почти не двигался
            {
                playerIsStanding = true;
                standingTime += Time.deltaTime;

                // Если стоит 2 секунды и анимация еще не началась
                if (standingTime >= 2f && !animationStarted)
                {
                    playerAnimator.SetBool("IsHandRaised", true);
                    animationStarted = true;
                    Debug.Log("Player stood for 2 seconds - hand raised");
                }
            }
            else
            {
                // Игрок двигается - сбрасываем таймер
                playerIsStanding = false;
                standingTime = 0f;
                if (animationStarted)
                {
                    playerAnimator.SetBool("IsHandRaised", false);
                    animationStarted = false;
                }
            }

            // Обновляем последнюю позицию для следующего кадра
            lastPlayerPosition = currentPlayerPosition;

            // Взаимодействие по нажатию E (всегда доступно)
            if (Input.GetKeyDown(KeyCode.E))
            {
                int currentIndex = SceneManager.GetActiveScene().buildIndex;

                if (goNext)
                    SceneManager.LoadScene(currentIndex + 1);
                else
                    SceneManager.LoadScene(currentIndex - 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = true;
            playerAnimator = collision.GetComponent<Animator>();
            lastPlayerPosition = collision.transform.position;
            standingTime = 0f;
            animationStarted = false;
            playerIsStanding = false;
            Debug.Log("Player near door");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsNear = false;
            playerIsStanding = false;
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("IsHandRaised", false);
            }
            playerAnimator = null;
            standingTime = 0f;
            animationStarted = false;
            Debug.Log("Player left door");
        }
    }
}