using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyShip : Ships
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemyDataSO enemyData;


    protected override void Start()
    {
        base.Start();
    }


    public void SetData(EnemyDataSO data)
    {
        enemyData = data;
        if (enemyData != null)
        {
            health = enemyData.Health;
            moveSpeed = enemyData.Speed;
           
            GetComponent<SpriteRenderer>().sprite = enemyData.sprite;
        }

        moveDirection = Vector2.down; 
    }

    protected override void Die()
    {
        Debug.Log(enemyData.EnemyName + " ha sido eliminado");
        base.Die();
    }
}
