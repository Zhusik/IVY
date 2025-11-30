using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;            // Animator на SpriteHolder
    public Collider2D hitbox;        // Триггер-хитбокс
    public SpriteRenderer sprite;    // Графика атаки

    [Header("Settings")]
    public float delayBeforeAttack = 0.1f; // задержка
    public float attackDuration = 0.5f;    // длина удара

    private void Awake()
    {
        sprite.enabled = false; // скрыть хлыст
        hitbox.enabled = false; // отключить урон
    }

    public void ActivateAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(delayBeforeAttack);

        sprite.enabled = true;
        hitbox.enabled = true;

        anim.Play("Attack"); // твой клип

        yield return new WaitForSeconds(attackDuration);

        sprite.enabled = false;
        hitbox.enabled = false;

        Destroy(gameObject);
    }
}
