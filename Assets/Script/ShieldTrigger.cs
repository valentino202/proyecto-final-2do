using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && !bullet.FirsBounce)
        {
            Destroy(bullet.gameObject);
        }
    }
}
