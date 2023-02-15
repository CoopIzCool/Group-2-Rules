using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Collider2D playerCollider = collision.collider;

            Vector3 contactPoint = collision.GetContact(0).point;
            Vector3 enemyBounds = gameObject.GetComponent<Collider2D>().bounds.max;
            Vector3 playerBounds = playerCollider.bounds.min;

            Debug.Log(Vector2.SignedAngle(gameObject.GetComponent<Collider2D>().bounds.center,contactPoint) * Mathf.Rad2Deg);
            if(playerBounds.y + 0.05 > enemyBounds.y)
            {
                EnemyDeath(collision.gameObject);
            }
            else
            {
                HurtPlayer(collision.gameObject);
            }
            
        }
    }

    protected virtual void EnemyDeath(GameObject Player)
    {
        Player.GetComponent<PlayerMovement>().HitEnemy();
    }

    protected virtual void HurtPlayer(GameObject Player)
    {
        Player.GetComponent<PlayerMovement>().Kill();
    }
}
