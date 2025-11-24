using System.Security.Cryptography;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullPrefab;
    public Transform firePoint1; // izquierda
    public Transform firePoint2; // derecha
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer = 0f;

    private int patternLeft = 0;  // patrón para firePoint1
    private int patternRight = 0; // patrón para firePoint2

    // Patrones de ángulo por cañón
    [SerializeField] private float[] leftAngles = { 50f, 90f };   // diagonal izquierda, vertical
    [SerializeField] private float[] rightAngles = { 130f, 90f }; // diagonal derecha, vertical

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            ShootFromBothPoints();
            fireTimer = 0f;
        }
    }

    void ShootFromBothPoints()
    {
        CreateBullet(firePoint1, leftAngles, ref patternLeft);
        CreateBullet(firePoint2, rightAngles, ref patternRight);
    }

    void CreateBullet(Transform firePoint, float[] angles, ref int patternIndex)
    {
        GameObject bulletObj = Instantiate(bullPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletClass = bulletObj.GetComponent<Bullet>();

        if (bulletClass != null)
        {
            bulletClass.shooter = this;
            bulletClass.originFirePoint = firePoint;

           
            bulletClass.initialAngle = angles[patternIndex];
            patternIndex = (patternIndex + 1) % angles.Length;

           
            if (firePoint == firePoint1)
            {
                bulletClass.initialColor = Color.cyan; // izquierda
                bulletClass.bounceColor = Color.red;  // después del rebote
            }
            else if (firePoint == firePoint2)
            {
                bulletClass.initialColor = Color.magenta; // derecha
                bulletClass.bounceColor = Color.red;      // después del rebote
            }
        }

        Rigidbody2D rb = bulletObj.GetComponent<Rigidbody2D>();

        float rad = bulletClass.initialAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        rb.linearVelocity = dir * bulletSpeed;
    }
}
