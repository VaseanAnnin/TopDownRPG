using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    public Rigidbody2D rb;

    public float MoveSpeedHorizontal;
    public float MoveSpeedVertical;
    public float facingDirectionUp=1;
    public float facingDirectionRight=1;


    private Animator anim;



    // Start is called before the first frame update
    void Start()
    {
       
    }
  
    // Update is called once per frame
    void Update()
 
    }

    public void MoveHorizontal()
    {
        float HZInput = Input.GetAxisRaw("Horizontal");
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
        if (HZInput < 0) {
            facingDirectionRight = -1;
        }
        else if(HZInput>0){
            facingDirectionRight = 1;
        }


      
    }

    public void MoveVertical()
    {
        float VInput = Input.GetAxisRaw("Vertical");
        //Similar to move Horizontal
        if (touchingTopBorder)
        {
            rb.velocity = new Vector2(rb.velocity.x, MoveSpeedVertical * VInput);
        }
        else {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
      
        if (VInput < 0)
        {
            facingDirectionUp = -1;
            //Trying to make sure the character doesn't go too far beyond the border
            touchingTopBorderDistance =-0.28f;
        }
        else if (VInput > 0)
        {
            touchingTopBorderDistance = -0.68f;
            facingDirectionUp = 1;
        }
    }




}

