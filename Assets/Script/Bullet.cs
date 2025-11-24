using System.Linq;
using System.Net;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Shooting shooter;
    public Transform originFirePoint;
   [SerializeField] private float velocity = 10f;
    Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool rebound = false;
    private bool firstBounce = true;
    [SerializeField]private int damage = 5;
    [HideInInspector] public float initialAngle = 90f;
    [HideInInspector] public Color initialColor = Color.cyan;
    [HideInInspector] public Color bounceColor = Color.red;
    public bool FirsBounce => firstBounce;

    private void Start()
    {   

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        if (shooter != null && originFirePoint != null)
        {
            Debug.Log("Bala creada desde: " + originFirePoint.name);
        }

        if (sr != null)
            sr.color = initialColor;


        float rad = initialAngle * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        rb.linearVelocity = dir * velocity;

    }

    private void Update()
    {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90f);   
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
                firstBounce = false; 


                if (sr != null)
                    sr.color = bounceColor;
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
        else
        {
            // Ignorar el escudo
            if (collision.gameObject.CompareTag("Shield"))
                return;

            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
            }

            Destroy(gameObject);
        }


    }
}