using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    public Rigidbody2D rb;

    public float MoveSpeedHorizontal;
    public float MoveSpeedVertical;

    public Animator anim;

    private bool facingRight = true;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }
  
    // Update is called once per frame
    void Update()
    {
        AnimationControllers();

        MoveHorizontal();
        MoveVertical();

        FlipControllerHorizontal();
        


    }

    private void AnimationControllers()
    {
        //.SetBool("isIdle", isIdle);
        bool isMovingHorizontal = rb.velocity.x != 0;
        anim.SetBool("isMovingHorizontal", isMovingHorizontal);

        bool isMovingDown = rb.velocity.y < 0;
        anim.SetBool("isMovingDown", isMovingDown);
    }

    public void MoveHorizontal()
    {
        rb.velocity = new Vector2(MoveSpeedHorizontal * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
    }

    public void MoveVertical()
    {
        rb.velocity = new Vector2(rb.velocity.x, MoveSpeedVertical * Input.GetAxisRaw("Vertical"));
    }

    private void FlipControllerHorizontal()
    {
        if (facingRight && rb.velocity.x < -.1f)
            FlipHorizontal();
        else if (!facingRight && rb.velocity.x > -.1f)
            FlipHorizontal();
    }

    

    private void FlipHorizontal()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    
}

