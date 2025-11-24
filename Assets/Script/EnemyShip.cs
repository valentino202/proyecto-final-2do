using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShip : Ships
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemyDataSO enemyData;

    [Header("Obstacle Avoidance")]
    [SerializeField] private float rayDistance = 0.5f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float avoidSpeedMultiplier = 1.2f; 

    private enum EnemyState
    {
        MovingDown,
        AvoidingObstacle
    }

    private EnemyState currentState = EnemyState.MovingDown;
    private Vector2 avoidDirection;
    private float avoidTimer = 0f;
    private float maxAvoidTime = 1.0f; 

    protected override void Start()
    {
        base.Start();

        if (enemyData != null)
        {
            health = enemyData.Health;
            moveSpeed = enemyData.Speed;
            GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
        }

        moveDirection = Vector2.down;
    }

    public void SetData(EnemyDataSO data)
    {
        enemyData = data;

        if (enemyData != null)
        {
            health = enemyData.Health;
            moveSpeed = enemyData.Speed;

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = enemyData.sprite;
        }

        moveDirection = Vector2.down;
    }

    protected override void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.MovingDown:
                DetectObstacle();
                rb.linearVelocity = moveDirection * moveSpeed;
                RotateToDirection(moveDirection);
                break;

            case EnemyState.AvoidingObstacle:
                avoidTimer += Time.fixedDeltaTime;
                rb.linearVelocity = avoidDirection * moveSpeed * avoidSpeedMultiplier;
                RotateToDirection(avoidDirection);

                if (avoidTimer >= maxAvoidTime)
                {
                    currentState = EnemyState.MovingDown;
                    moveDirection = Vector2.down;
                }
                break;
        }
    }

    private void DetectObstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, obstacleLayer);
        if (hit.collider != null)
        {
           
            Vector2 perp = Vector2.Perpendicular(moveDirection).normalized;
           
            int side = Random.value < 0.5f ? 1 : -1;
            Vector2 chosenPerp = perp * side;

            RaycastHit2D check = Physics2D.Raycast(transform.position, chosenPerp, rayDistance, obstacleLayer);

            if (check.collider == null)
                avoidDirection = (moveDirection + chosenPerp).normalized;
            else
                avoidDirection = (moveDirection - chosenPerp).normalized; 

            currentState = EnemyState.AvoidingObstacle;
            avoidTimer = 0f;
        }
    }

    private void RotateToDirection(Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            ShieldController shield = collision.gameObject.GetComponentInParent<ShieldController>();
            if (shield != null)
            {
                shield.TakeDamage(enemyData.Damage);
            }

            Die();
            return;
        }

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(enemyData.Damage);
        }
    }

    protected override void Die()
    {
        Debug.Log(enemyData.EnemyName + " ha sido eliminado");

        
        UIGameManager uiManager = Object.FindFirstObjectByType<UIGameManager>();
        if (uiManager != null)
        {
            uiManager.AñadirKill();
        }

        base.Die();
    }
}
