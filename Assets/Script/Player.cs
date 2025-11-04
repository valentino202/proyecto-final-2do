using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Ships
{
    protected override void Start()
    {
        base.Start();
    }


    void Update()
    {
       
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

   
}
