using UnityEngine;

public class MoveBetweenPointsComponent : MonoBehaviour
{
    [Header("Desplazamientos relativos")]
    public Vector2 offsetA = new Vector2(-200, 0);
    public Vector2 offsetB = new Vector2(200, 0);

    [Header("Velocidad")]
    public float velocidad = 100f;

    private IMovementBehavior movimiento;
    private Vector2 puntoA;
    private Vector2 puntoB;

    void Start()
    {
        // Calculamos los puntos RELATIVOS a la posición inicial
        Vector2 startPos;

        RectTransform rect = GetComponent<RectTransform>();
        if (rect != null)
            startPos = rect.anchoredPosition;
        else
            startPos = transform.position;

        puntoA = startPos + offsetA;
        puntoB = startPos + offsetB;

        movimiento = new MoveBetweenPoints(puntoA, puntoB, velocidad);
    }

    void Update()
    {
        movimiento.Move(transform);
    }
}
