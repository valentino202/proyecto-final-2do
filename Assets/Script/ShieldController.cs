using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour, IDamageable
{
    public Transform player;
    [SerializeField] private float radius = 2f;

    [Header("Shield Settings")]
    public int maxHealth = 3;         // Vida máxima
    [SerializeField] private float respawnTime = 3f;

    private int currentHealth;
    private bool shieldActive = true;

    public int CurrentHealth => currentHealth;

    [Header("References to shields")]
    public GameObject shieldMain; // Objeto padre
    public GameObject shieldChild; // Hijo Shield2

    private SpriteRenderer srMain;
    private Collider2D colMain;
    private SpriteRenderer srChild;
    private Collider2D colChild;

    private void Start()
    {
        currentHealth = maxHealth;

        srMain = shieldMain.GetComponent<SpriteRenderer>();
        colMain = shieldMain.GetComponent<Collider2D>();

        srChild = shieldChild.GetComponent<SpriteRenderer>();
        colChild = shieldChild.GetComponent<Collider2D>();

        EnableShields(true);
    }

    private void FixedUpdate()
    {
        if (player == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 direction = (mousePos - player.position).normalized;

        shieldMain.transform.position = player.position + direction * radius;
        shieldChild.transform.position = shieldMain.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        shieldMain.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        shieldChild.transform.rotation = shieldMain.transform.rotation;
    }


    public void TakeDamage(int amount)
    {
        if (!shieldActive) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            StartCoroutine(RespawnShields());
        }
    }

    private IEnumerator RespawnShields()
    {
        EnableShields(false);         // Desactiva ambos escudos
        yield return new WaitForSeconds(respawnTime);
        currentHealth = maxHealth;
        EnableShields(true);          // Reactiva ambos escudos
    }

    private void EnableShields(bool enable)
    {
        shieldActive = enable;

        if (srMain != null) srMain.enabled = enable;
        if (colMain != null) colMain.enabled = enable;

        if (srChild != null) srChild.enabled = enable;
        if (colChild != null) colChild.enabled = enable;
    }
}
