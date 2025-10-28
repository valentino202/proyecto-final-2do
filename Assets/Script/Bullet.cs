using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounce"))
        {
            Debug.Log("la bala colicciono con el " + collision.gameObject + " puede rebotar");

            ContactPoint2D contact = collision.contacts[0];
            print("Punto de contacto: " + contact.point);


            Vector2 vl = rb.linearVelocity;
            Vector2 normal = collision.contacts[0].normal;


             rb.linearVelocity = Vector2.zero;
            Debug.Log("la velocidad se mantine zero");

            float magnitud = vl.magnitude;
            Vector2 nuevaDireccion = Vector2.zero;

            if (Mathf.Abs(normal.x) == Mathf.Abs(normal.y))
            {

            }


        }
        else if (!collision.gameObject.CompareTag("Bounce"))
        {
            Debug.Log("la bala no puede rebotar en "+ collision.gameObject);
            Destroy(gameObject);
        }

    }
}