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
    private float speed = 1.4f;
    private Vector2 velocity;
    private Vector2 bottomLocal;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private const float groundedTolerance = 0.75f;
    private const float edgeTolerance = 0.75f; // Distance that is considered an edge/cliff
    [SerializeField][Range(0.0f,2.0f)] private float wallTolerance = 0.2f; // Distance too a wall/object
    [SerializeField] private Vector2 centerOffset = new Vector3(0.0f, 0f);
    private Vector2 rayOffset = new Vector3(0.9f, 0f);
    private Vector2 groundOffset = new Vector3(0f, 0.3f);

    private bool IsGrounded
    {
        get
        {
            RaycastHit2D underObject = Physics2D.Raycast((Vector2)transform.position, Vector3.down, groundedTolerance, groundLayer);
            //Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
            if (underObject.collider) return true;
            return false;
        }
    }
    private int IsByEdge
    {
        get
        {
            RaycastHit2D rayRightHit = Physics2D.Raycast((Vector2)transform.position + rayOffset, Vector3.down, edgeTolerance, groundLayer);
            if (!rayRightHit.collider) return 1;

            RaycastHit2D rayLeftHit = Physics2D.Raycast((Vector2)transform.position - rayOffset, Vector3.down, edgeTolerance, groundLayer);
            if (!rayLeftHit.collider) return 2;

            return 0;
        }
    }
    private int IsByWall
    {
        get
        {
            RaycastHit2D rayRightHit = Physics2D.Raycast((Vector2)transform.position + centerOffset + rayOffset, Vector3.right, wallTolerance, wallLayer);
            if (rayRightHit.collider) return 1;

            RaycastHit2D rayLeftHit = Physics2D.Raycast((Vector2)transform.position + centerOffset - rayOffset, Vector3.left, wallTolerance,wallLayer);
            if (rayLeftHit.collider) return 2;
            return 0;
        }
    }

    private void Awake() // will need to remove monogame by renaming
    {
        rigidBody = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        bottomLocal.y = collider.bounds.min.y;
        velocity = new Vector2(speed, 0f);
    }

    public void Updatee()
    {
        if (debug)
        {
            Debug.DrawRay((Vector2)transform.position + bottomLocal, Vector3.down * groundedTolerance, Color.red);
            if(type == CSType.EDGE)
            {
                Debug.DrawRay((Vector2)transform.position + rayOffset, Vector3.down * edgeTolerance, Color.blue);
                Debug.DrawRay((Vector2)transform.position - rayOffset, Vector3.down * edgeTolerance, Color.blue);
            }
            else if (type == CSType.WALL)
            {
                Debug.DrawRay((Vector2)transform.position + centerOffset + rayOffset, Vector3.right * wallTolerance, Color.blue);
                Debug.DrawRay((Vector2)transform.position + centerOffset - rayOffset, Vector3.left * wallTolerance, Color.blue);
            }
        }

        if (IsGrounded) 
        { 
            state = EnemyState.MOVING;

            switch (type)
            {
                case CSType.EDGE:
                    int isByEdge = IsByEdge;
                    if (isByEdge > 0)
                    {
                        if(isByEdge == 1) { velocity = new Vector2(-speed, 0f); }
                        else { velocity = new Vector2(speed, 0f); }
                    }
                    break;
                case CSType.WALL:
                    int isBywall = IsByWall;
                    if (isBywall > 0)
                    {
                        if (isBywall == 1) { velocity = new Vector2(-speed, 0f); }
                        else { velocity = new Vector2(speed, 0f); }
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
