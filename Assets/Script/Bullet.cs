using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rebountSpread = 15f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bounce"))
        {
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;
            Vector2 incomingVelocity = rb.linearVelocity;

            Vector2 reflecterDireccion = Vector2.Reflect(incomingVelocity, normal);
            float reflectedAngle = Mathf.Atan2(reflecterDireccion.y, reflecterDireccion.x) * Mathf.Rad2Deg;


            float randomOffset = Random.Range(-rebountSpread,rebountSpread);
            float newAngle = reflectedAngle + randomOffset;

            Vector2 NewDireccion = new Vector2(Mathf.Cos(newAngle * Mathf.Deg2Rad),Mathf.Sin(newAngle * Mathf.Deg2Rad)); 

            rb.linearVelocity = NewDireccion.normalized * speed;

            Debug.DrawRay(transform.position, NewDireccion * 2f, Color.red, 1);
            Debug.Log($"Rebote → Ángulo base: {reflectedAngle:F1}°, Desviación: {randomOffset:F1}°, Final: {newAngle:F1}°");
        }
    }
    public void SerVelocity(Vector2 direccion, float newSeed)
    {
        speed = newSeed;
        rb.linearVelocity = direccion.normalized * speed;
    }

}