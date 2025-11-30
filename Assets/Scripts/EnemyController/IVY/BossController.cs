using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public Transform player;            // Игрок
    public GameObject whipPrefab;       // Префаб атаки (хлыст)
    public Transform attackPoint;       // Точка спавна атаки

    [Header("Settings")]
    public float visionRange = 5f;      // Радиус обнаружения
    public float attackCooldown = 2f;   // КД атаки

    private bool canAttack = true;
    private Animator anim;              // Animator сердца

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= visionRange)
        {
            anim.SetBool("Aggro", true);
            LookAtPlayer();

            if (canAttack)
                DoAttack();
        }
        else
        {
            anim.SetBool("Aggro", false);
        }
    }

    private void LookAtPlayer()
    {
        // если игрок слева
        if (player.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // игрок справа
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void DoAttack()
    {
        anim.SetTrigger("Attack"); // анимация сердца

        // создаём хлыст
        GameObject atk = Instantiate(whipPrefab, attackPoint.position, Quaternion.identity);

        // разворачиваем хлыст в сторону игрока
        bool playerLeft = player.position.x < transform.position.x;

        if (playerLeft)
            atk.transform.localScale = new Vector3(-1, 1, 1);
        else
            atk.transform.localScale = new Vector3(1, 1, 1);

        atk.GetComponent<BossAttack>().ActivateAttack();

        // запускаем КД
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
