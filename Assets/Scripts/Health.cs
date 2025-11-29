using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3;
    public int health;

    private PlayerMovement player;

    private void Start()
    {
        health = maxHealth;
        player = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            if (player != null)
            {
                player.Die();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
