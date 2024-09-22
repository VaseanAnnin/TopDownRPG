using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Script : Entity
{
    //test
    public float MoveSpeedHorizontal;
    public float MoveSpeedVertical;

    public bool test;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        touchingRightBorderDistance = 0.48f;
        distanceFromCenterH = 0.16f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Debug.Log("Code is running");

        AnimationControllers();
        collisionChecks();

        MoveHorizontal();
        MoveVertical();

        FlipControllerHorizontal();

        //OnDrawGizmos();
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
        //enables sprinting when the left shift key is held
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
        }
        else if (VInput > 0)
        {
            facingDirectionUp = 1;
        }
    }
}
