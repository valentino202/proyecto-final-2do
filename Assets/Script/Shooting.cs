using System.Security.Cryptography;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    [SerializeField] float bullletSeep = 10f;
    Rigidbody2D rb;

    public float BulletSeep => bullletSeep;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootFromBothPoints();
        }
    }


    void ShootFromBothPoints()
     {
        
        CreateBullet(firePoint1);
        CreateBullet(firePoint2);

     }

   
    void CreateBullet(Transform firePoint)
    {
        GameObject bulletOdjt = Instantiate(bullPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletClass = bulletOdjt.GetComponent<Bullet>();
        if (bulletClass != null)
        {
            bulletClass.shooter = this;
            bulletClass.originFirePoint = firePoint;
        }

        rb = bulletOdjt.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.up * bullletSeep;
    }
   
}
