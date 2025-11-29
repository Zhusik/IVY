using UnityEngine;
using System.Collections;

public class OnionController : MonoBehaviour
{
    [Header("Player target")]
    public Health playerHealth;
    public int damage = 3;

    [Header("Watch settings")]
    [SerializeField] float watchTime = 3f;
    [SerializeField] float restTime = 2f;
    [SerializeField] float targetHeight = 4.2f;

    [Header("Attack Range")]
    [SerializeField] float attackRange = 5f;
    [SerializeField] Color attackColor = Color.red;

    private Animator anim;
    private bool isActive = true;
    private bool isWatching = false;
    private Light attackLight;

    private const string WATCH_PARAM = "isWatching";

    private void Start()
    {
        Vector3 p = transform.position;
        p.y = targetHeight;
        transform.position = p;

        anim = GetComponent<Animator>();

        // Простой свет для показа дистанции
        attackLight = gameObject.AddComponent<Light>();
        attackLight.type = LightType.Point;
        attackLight.range = attackRange;
        attackLight.color = attackColor;
        attackLight.intensity = 0f;

        StartCoroutine(WatchCycle());
    }

    private IEnumerator WatchCycle()
    {
        while (isActive)
        {
            // Отдых
            isWatching = false;
            anim.SetBool(WATCH_PARAM, false);
            attackLight.intensity = 0f;
            yield return new WaitForSeconds(restTime);

            // Атака
            isWatching = true;
            anim.SetBool(WATCH_PARAM, true);
            attackLight.intensity = 2f; // Включаем свет
            yield return new WaitForSeconds(watchTime);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isWatching)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}