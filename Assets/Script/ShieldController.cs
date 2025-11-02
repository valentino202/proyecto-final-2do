using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public Transform player;
    [SerializeField]private float radius = 2f;

    private void FixedUpdate()
    {
        if (player == null) return;

     
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;


        Vector3 direction = (mousePos - player.position).normalized;

       
        transform.position = player.position + direction * radius;

      
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); 


    }
}
