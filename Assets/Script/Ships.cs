using UnityEngine;

public abstract class Ships : MonoBehaviour, IDamageable
{
    [Header("Ship Settings")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected int health;

    protected Rigidbody2D rb;
    protected Vector2 moveDirection;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
