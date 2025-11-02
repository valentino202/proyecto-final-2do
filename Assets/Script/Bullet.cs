using System.Linq;
using System.Net;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Shooting shooter;
    public Transform originFirePoint;
   [SerializeField] private float velocity = 10f;
    Rigidbody2D rb;
    private bool rebound = false;
    private bool firstBounce = true;


    public bool FirsBounce => firstBounce;
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
        if (rebound && rb != null && rb.linearVelocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bounce"))
        {
   
            ContactPoint2D contact = collision.contacts[0];
            rebound = true; 

            Vector2 dir = rb.linearVelocity.normalized;
            Vector2 normal = contact.normal;

            if(firstBounce)
            {
                int x = (originFirePoint == shooter.firePoint1) ? 1 : -1;

                int y = (normal.y < 0) ? 1 : -1;
                dir = new Vector2(x, y).normalized;
                firstBounce = false;
            }
            else
            {
                if(Mathf.Abs(normal.x)> 0.5f)
                    dir.x *=-1;
                if (Mathf.Abs(normal.y) > 0.5f)
                    dir.y *=-1;
            }
           
           rb.linearVelocity = dir * velocity;
           

        }
        else if (!collision.gameObject.CompareTag("Bounce"))
        {
            Debug.Log("la bala no puede rebotar en "+ collision.gameObject);
            Destroy(gameObject);
        }

    }
}