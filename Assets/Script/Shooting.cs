using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullPrefab;
    public Transform firePoint1;
    public Transform firePoint2;
    [SerializeField] float bullletSeep = 10f;

    public float BulletSeep => bullletSeep;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject Bullet1 = Instantiate(bullPrefab, firePoint1.position, Quaternion.identity);
        Rigidbody2D rb1 = Bullet1.GetComponent<Rigidbody2D>();
        rb1.linearVelocity = Vector2.up * bullletSeep;

        GameObject Bullet2 = Instantiate(bullPrefab, firePoint2.position, Quaternion.identity);
        Rigidbody2D rb2 = Bullet2.GetComponent<Rigidbody2D>();
        rb2.linearVelocity = Vector2.up * bullletSeep;
    }

}
