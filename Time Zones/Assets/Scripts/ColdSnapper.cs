using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class ColdSnapper : EnemyBehavior
{
    public enum CSType
    {
        EDGE,
        WALL
    }
    public CSType type;
    public bool debug = false;
    private EnemyState state = EnemyState.INIT;
    private Rigidbody2D rigidBody;
    private float speed = 0.4f;
    private Vector2 velocity;
    private Vector2 spriteBottomLocal;
    private LayerMask groundLayer;

    private const float groundedTolerance = 0.75f;
    private const float edgeTolerance = 0.75f; // Distance that is considered an edge/cliff
    private const float wallTolerance = 0.2f; // Distance too a wall/object
    private Vector2 rayOffset = new Vector3(0.6f, 0f);

    //public GameObject target;

    private bool IsGrounded
    {
        get
        {
            RaycastHit2D underObject = Physics2D.Raycast((Vector2)transform.position + spriteBottomLocal, Vector3.down, groundedTolerance, groundLayer);
            //Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
            if (underObject.collider) return true;
            return false;
        }
    }
    private bool IsByEdge
    {
        get
        {
            RaycastHit2D rayRightHit = Physics2D.Raycast((Vector2)transform.position + rayOffset, Vector3.down, edgeTolerance, groundLayer);
            if (!rayRightHit.collider) return true;

            RaycastHit2D rayLeftHit = Physics2D.Raycast((Vector2)transform.position - rayOffset, Vector3.down, edgeTolerance, groundLayer);
            if (!rayLeftHit.collider) return true;
            return false;
        }
    }
    private bool IsByWall
    {
        get
        {
            RaycastHit2D rayRightHit = Physics2D.Raycast((Vector2)transform.position + rayOffset, Vector3.right, wallTolerance);
            if (rayRightHit.collider) return true;

            RaycastHit2D rayLeftHit = Physics2D.Raycast((Vector2)transform.position - rayOffset, Vector3.left, wallTolerance);
            if (rayLeftHit.collider) return true;
            return false;
        }
    }

    private void Awake() // will need to remove monogame by renaming
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteBottomLocal.y = -sr.size.y/2;
        velocity = new Vector2(speed, 0f);

        //target = GameObject.FindGameObjectWithTag("Player");

        state = EnemyState.STATIONARY;
    }

    public void Updatee()
    {
        Debug.Log(IsGrounded + "is the grounding value for " + name);
        if (debug)
        {
            Debug.DrawRay((Vector2)transform.position + spriteBottomLocal, Vector3.down * groundedTolerance, Color.red);
            if(type == CSType.EDGE)
            {
                Debug.DrawRay((Vector2)transform.position + rayOffset, Vector3.down * edgeTolerance, Color.blue);
                Debug.DrawRay((Vector2)transform.position - rayOffset, Vector3.down * edgeTolerance, Color.blue);
            }
            else if (type == CSType.WALL)
            {
                Debug.DrawRay((Vector2)transform.position + rayOffset, Vector3.right * wallTolerance, Color.blue);
                Debug.DrawRay((Vector2)transform.position - rayOffset, Vector3.left * wallTolerance, Color.blue);
            }
        }

        if (IsGrounded) 
        { 
            state = EnemyState.MOVING;

            switch (type)
            {
                case CSType.EDGE:
                    if (IsByEdge)
                    {
                        velocity = -velocity; // Change directions
                    }
                    break;
                case CSType.WALL:
                    if (IsByWall)
                    {
                        velocity = -velocity; // Change directions
                    }
                    break;
            }
        }
        else 
        {
            state = EnemyState.STATIONARY;
        }
    }

    public void FixedUpdatee()
    {
        switch (state)
        {
            case EnemyState.MOVING:
                rigidBody.position += velocity * Time.deltaTime;
                break;
            case EnemyState.STATIONARY:
                // Don't move
                break;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void EnemyDeath(GameObject Player)
    {
        base.EnemyDeath(Player);
        gameObject.SetActive(false);
    }

    protected override void HurtPlayer(GameObject Player)
    {
        base.HurtPlayer(Player);
    }
}
