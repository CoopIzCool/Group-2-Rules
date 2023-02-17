using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PlatformerActor : EnemyBehavior
{
    private float xLeftBound;
    private float xRightBound;
    private float yCurrent;
    [SerializeField]
    private float speed;
    private bool movingRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
        xLeftBound = transform.Find("LeftBound").position.x;
        xRightBound = transform.Find("RightBound").position.x;
    }

    // Update is called once per frame
    public void Update()
    {
        if (movingRight)
        {
            if (transform.position.x <= xRightBound)
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y );
            else
                movingRight = !movingRight;

        }
        else
        {
            if (transform.position.x >= xLeftBound)
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y );
            else
                movingRight = !movingRight;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void EnemyDeath(GameObject Player)
    {
        //base.EnemyDeath(Player);
    }

    /*protected override void EnemyDeath(GameObject Player)
    {
        base.EnemyDeath(Player);
        gameObject.SetActive(false);
    }*/
}
