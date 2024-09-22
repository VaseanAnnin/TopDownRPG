using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    [Header("Collision Info")]
    [SerializeField]
    protected LayerMask whatIsBorder;

    [SerializeField]
    protected float distanceFromCenterV;

    [SerializeField]
    protected float distanceFromCenterH;

    [SerializeField]
    protected bool touchingTopBorder;

    [SerializeField]
    protected bool touchingRightBorder;

    [SerializeField]
    protected float touchingTopBorderDistance;

    [SerializeField]
    protected float touchingRightBorderDistance;
    public Vector2 currentPos;

    protected float facingDirectionUp = 1;
    protected float facingDirectionRight = 1;

    [SerializeField]
    protected bool facingRight = true;

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        currentPos = rb.transform.position;
    }

    protected virtual void Update()
    {
        if (rb.transform.position.y > currentPos.y)
        {
            touchingTopBorderDistance = -0.68f;
        }
        else if (rb.transform.position.y < currentPos.y)
        {
            touchingTopBorderDistance = -0.28f;
        }
        currentPos = rb.transform.position;
    }

    protected virtual void collisionChecks()
    {
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

        touchingTopBorder = Physics2D.Raycast(
            verticalRayOrigin,
            Vector2.down,
            touchingTopBorderDistance * facingDirectionUp,
            whatIsBorder
        );

        touchingRightBorder = Physics2D.Raycast(
            horizontalRayOrigin,
            Vector2.right,
            (touchingRightBorderDistance - distanceFromCenterH) * facingDirectionRight,
            whatIsBorder
        );
    }

    protected virtual void OnDrawGizmos()
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

        Gizmos.DrawLine(
            verticalRayOrigin,
            new Vector3(
                transform.position.x,
                transform.position.y - touchingTopBorderDistance * facingDirectionUp
            )
        );

        Gizmos.DrawLine(
            horizontalRayOrigin,
            new Vector3(
                touchingRightBorderDistance * facingDirectionRight + transform.position.x,
                transform.position.y
            )
        );
        // Gizmos.DrawLine(transform.position, new Vector3(touchingRightBorderDistance*facingDirectionRight + transform.position.x, transform.position.y));
    }

    protected virtual void FlipControllerHorizontal()
    {
        if (facingRight && rb.velocity.x < -.1f)
            FlipHorizontal();
        else if (!facingRight && rb.velocity.x > .1f)
            FlipHorizontal();
    }

    protected virtual void FlipHorizontal()
    {
        facingDirectionRight = facingDirectionRight * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /*
    protected virtual void canMove(float movementSpeed) {
        if (!touchingTopBorder)
        {
            rb.transform.position.y
        }
    }*/
}
