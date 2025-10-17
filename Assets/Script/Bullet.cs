using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bounce"))
        {
            BounceRandomlyUpward();
        }
    }

    void BounceRandomlyUpward()
    {

        float angle = Random.Range(35f, 360f);

        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Debug.Log($"Rebote con ángulo {angle}° y dirección {direction}");
        rb.linearVelocity = direction.normalized * speed;



        transform.position += (Vector3)(direction.normalized * 0.01f);
    }

  
    public void SetVelocity(Vector2 direction, float newSpeed)
    {
        speed = newSpeed;
        rb.linearVelocity = direction.normalized * speed;
    }
}