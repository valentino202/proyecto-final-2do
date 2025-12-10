using UnityEngine;

public class MoveBetweenPoints :  IMovementBehavior
{
    private Vector2 pointA;
    private Vector2 pointB;
    private float speed;
    private bool goingToB = true;

    public MoveBetweenPoints(Vector2 a, Vector2 b, float spd)
    {
        pointA = a;
        pointB = b;
        speed = spd;
    }

    public void Move(Transform obj)
    {
    
        RectTransform rect = obj as RectTransform;

        if (rect != null)
        {
            MoveUI(rect);
        }
        else
        {
            MoveWorld(obj);
        }
    }

    private void MoveWorld(Transform obj)
    {
        Vector2 target = goingToB ? pointB : pointA;

        obj.position = Vector2.MoveTowards(
            obj.position,
            target,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(obj.position, target) < 0.1f)
            goingToB = !goingToB;
    }

    private void MoveUI(RectTransform rect)
    {
        Vector2 target = goingToB ? pointB : pointA;

        rect.anchoredPosition = Vector2.MoveTowards(
            rect.anchoredPosition,
            target,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(rect.anchoredPosition, target) < 0.1f)
            goingToB = !goingToB;
    }
}
