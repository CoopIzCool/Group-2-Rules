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
            Vector3 playerBounds = playerCollider.bounds.center;

            Vector3 impactDirection = (playerBounds - this.gameObject.GetComponent<Collider2D>().bounds.center).normalized;
            float angleOfImpact = Mathf.Atan2(impactDirection.y, impactDirection.x) * Mathf.Rad2Deg;
            Debug.Log(impactDirection);
            Debug.Log(angleOfImpact);
            if (angleOfImpact > 35 && angleOfImpact < 145)
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
