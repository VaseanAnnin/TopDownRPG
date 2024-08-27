using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Script : MonoBehaviour
{

    public Rigidbody2D rb;

    public float MoveSpeedHorizontal;
    public float MoveSpeedVertical;

    private Animator anim;


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

       
    }

    private void AnimationControllers()
    {
        //.SetBool("isIdle", isIdle);
    }

    public void MoveHorizontal()
    {
        rb.velocity = new Vector2(MoveSpeedHorizontal * Input.GetAxisRaw("Horizontal"), rb.velocity.y);
    }

    public void MoveVertical()
    {
        rb.velocity = new Vector2(rb.velocity.x, MoveSpeedVertical * Input.GetAxisRaw("Vertical"));
    }
}

