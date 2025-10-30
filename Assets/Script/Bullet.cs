using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Shooting shooter;
    public Transform originFirePoint;
   [SerializeField] private float velocity = 10f;
    Rigidbody2D rb;
    private bool reboto = false;


    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        if (shooter != null && originFirePoint != null)
        {
            Debug.Log("Bala creada desde: " + originFirePoint.name);
        }
    }

    private void Update()
    {
        if (reboto && rb != null && rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounce"))
        {
            Debug.Log("la bala colicciono con el " + collision.gameObject + " puede rebotar");
            ContactPoint2D contact = collision.contacts[0];
            print("Punto de contacto: " + contact.point);
            reboto = true;

            int x = (originFirePoint == shooter.firePoint1) ? 1 : -1;

            int y = (contact.point.y > transform.position.y) ? -1 : 1;

            Vector2 nuevaDireccion = new Vector2(x, y).normalized;
            rb.linearVelocity = nuevaDireccion * velocity;

            Debug.Log($"Rebote manual con dirección:" + nuevaDireccion);

        }
        else if (!collision.gameObject.CompareTag("Bounce"))
        {
            Debug.Log("la bala no puede rebotar en "+ collision.gameObject);
            Destroy(gameObject);
        }

    }
}