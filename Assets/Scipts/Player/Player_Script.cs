using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    public Rigidbody2D rb;
    public LayerMask whatIsBorder;

    public float MoveSpeedHorizontal;
    public float MoveSpeedVertical;
    public float facingDirectionUp=1;
    public float facingDirectionRight=1;

    [Header("Collision Info")]
    public bool touchingTopBorder;
    public bool touchingRightBorder;
    public float touchingTopBorderDistance;
    public float touchingRightBorderDistance;


    // Start is called before the first frame update
    void Start()
    {
       
    }
  
    // Update is called once per frame
    void Update()
    {
        collisionChecks();
        MoveHorizontal();
        MoveVertical();
        OnDrawGizmos();
    }

    public void MoveHorizontal()
    {

        if (touchingRightBorder)
        {
            rb.velocity = new Vector2(MoveSpeedHorizontal * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        }
        if (Input.GetAxisRaw("Horizontal") < 0) {
            facingDirectionRight = -1;
                }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            facingDirectionRight = 1;
        }
    }

    public void MoveVertical()
    {

        if (touchingTopBorder)
        {
            rb.velocity = new Vector2(rb.velocity.x, MoveSpeedVertical * Input.GetAxisRaw("Vertical"));
        }
        else { MoveSpeedVertical = 0; }
      
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            facingDirectionUp = -1;
            touchingTopBorderDistance =-0.48f;
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            touchingTopBorderDistance = -0.68f;
            facingDirectionUp = 1;
        }
    }

    public void collisionChecks() {
        touchingTopBorder = Physics2D.Raycast(transform.position, Vector2.down, touchingTopBorderDistance*facingDirectionUp, whatIsBorder);
        touchingRightBorder = Physics2D.Raycast(transform.position, Vector2.right, touchingRightBorderDistance*facingDirectionRight, whatIsBorder);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - touchingTopBorderDistance*facingDirectionUp));
        Gizmos.DrawLine(transform.position, new Vector3(touchingRightBorderDistance*facingDirectionRight + transform.position.x, transform.position.y));
       // Gizmos.DrawLine(transform.position, new Vector3(touchingRightBorderDistance*facingDirectionRight + transform.position.x, transform.position.y));
    }
}

