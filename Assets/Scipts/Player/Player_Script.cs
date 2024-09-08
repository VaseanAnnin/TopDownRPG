using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Script : MonoBehaviour
{
    public Rigidbody2D rb;

    //test
    public float MoveSpeedHorizontal;
    public float MoveSpeedVertical;
    public float facingDirectionUp = 1;
    public float facingDirectionRight = 1;

    [Header("Collision Info")]
    public LayerMask whatIsBorder;
    public float distanceFromCenterV;
    public float distanceFromCenterH;

    public bool touchingTopBorder;
    public bool touchingRightBorder;
    public float touchingTopBorderDistance;
    public float touchingRightBorderDistance;

    public Animator anim;

    private bool facingRight = true;
    private int facingDirection = 1;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Code is running");

        AnimationControllers();
        collisionChecks();

        MoveHorizontal();
        MoveVertical();

        FlipControllerHorizontal();

        OnDrawGizmos();
    }

    private void AnimationControllers()
    {
        //.SetBool("isIdle", isIdle);
        bool isMovingHorizontal = rb.velocity.x != 0;
        anim.SetBool("isMovingHorizontal", isMovingHorizontal);

        bool isMovingDown = rb.velocity.y < 0;
        anim.SetBool("isMovingDown", isMovingDown);

        bool isMovingUp = rb.velocity.y > 0;
        anim.SetBool("isMovingUp", isMovingUp);
    }

    public void MoveHorizontal()
    {
        float HZInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveSpeedHorizontal = 3;
        }
        else
        {
            MoveSpeedHorizontal = 1;
        }
        //If the character is within the bounds it can move in that direction
        if (touchingRightBorder)
        {
            rb.velocity = new Vector2(MoveSpeedHorizontal * HZInput, rb.velocity.y);
        }
        //If not then it can no longer move past that point
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        //Switches the RayCast so that the character can walk back within the bounds
        if (HZInput < 0)
        {
            facingDirectionRight = -1;
        }
        else if (HZInput > 0)
        {
            facingDirectionRight = 1;
        }
    }

    public void MoveVertical()
    {
        float VInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MoveSpeedVertical = 3;
        }
        else
        {
            MoveSpeedVertical = 1;
        }
        //Similar to move Horizontal
        if (touchingTopBorder)
        {
            rb.velocity = new Vector2(rb.velocity.x, MoveSpeedVertical * VInput);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (VInput < 0)
        {
            facingDirectionUp = -1;
            //Trying to make sure the character doesn't go too far beyond the border
            touchingTopBorderDistance = -0.28f;
        }
        else if (VInput > 0)
        {
            touchingTopBorderDistance = -0.68f;
            facingDirectionUp = 1;
        }
    }

    public void collisionChecks()
    {
        //Debug.Log(transform.position.y);
        Vector2 verticalRayOrigin;

        Vector2 horizontalRayOrigin;

        if (facingDirectionRight == -1)
        {
            verticalRayOrigin = new Vector2(
                transform.position.x + 0.6f,
                transform.position.y - distanceFromCenterV
            );
        }
        else
        {
            verticalRayOrigin = new Vector2(
                transform.position.x - 0.6f,
                transform.position.y - distanceFromCenterV
            );
        }
        if (facingDirectionUp == -1)
        {
            distanceFromCenterH = Mathf.Abs(distanceFromCenterH) * -1;
            horizontalRayOrigin = new Vector2(
                transform.position.x,
                transform.position.y - distanceFromCenterH
            );
        }
        else
        {
            distanceFromCenterH = Mathf.Abs(distanceFromCenterH);
            horizontalRayOrigin = new Vector2(
                transform.position.x,
                transform.position.y - distanceFromCenterH
            );
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            touchingTopBorder = Physics2D.Raycast(
                verticalRayOrigin,
                Vector2.down,
                touchingTopBorderDistance * facingDirectionUp,
                whatIsBorder
            );
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            touchingTopBorder = Physics2D.Raycast(
                verticalRayOrigin,
                Vector2.down,
                touchingTopBorderDistance * facingDirectionUp,
                whatIsBorder
            );
        }

        touchingRightBorder = Physics2D.Raycast(
            horizontalRayOrigin,
            Vector2.right,
            (touchingRightBorderDistance - distanceFromCenterH) * facingDirectionRight,
            whatIsBorder
        );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 verticalRayOrigin = new Vector2(
            transform.position.x,
            transform.position.y - distanceFromCenterV
        );
        Vector2 horizontalRayOrigin = new Vector2(
            transform.position.x,
            transform.position.y - distanceFromCenterH
        );
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            Gizmos.DrawLine(
                verticalRayOrigin,
                new Vector3(
                    transform.position.x,
                    transform.position.y - touchingTopBorderDistance * facingDirectionUp
                )
            );
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            Gizmos.DrawLine(
                transform.position,
                new Vector3(
                    transform.position.x,
                    transform.position.y - touchingTopBorderDistance * facingDirectionUp
                )
            );
        }
        Gizmos.DrawLine(
            horizontalRayOrigin,
            new Vector3(
                touchingRightBorderDistance * facingDirectionRight + transform.position.x,
                transform.position.y
            )
        );
        // Gizmos.DrawLine(transform.position, new Vector3(touchingRightBorderDistance*facingDirectionRight + transform.position.x, transform.position.y));
    }

    private void FlipControllerHorizontal()
    {
        if (facingRight && rb.velocity.x < -.1f)
            FlipHorizontal();
        else if (!facingRight && rb.velocity.x > .1f)
            FlipHorizontal();
    }

    private void FlipHorizontal()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
}
