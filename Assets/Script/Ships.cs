using UnityEngine;

public abstract class Ships : MonoBehaviour, IDamageable
{
    [Header("Ship Settings")]
    [SerializeField] protected float moveSeed = 5F;
    [SerializeField] protected int health = 10;

    protected Rigidbody2D rb;
    protected Vector2 moveDireccion;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = moveDireccion *moveSeed;
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
