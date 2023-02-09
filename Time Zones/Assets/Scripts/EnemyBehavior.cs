using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Is player");
            Collider2D playerCollider = collision.collider;

            Vector3 contactPoint = collision.GetContact(0).point;
            Vector3 playerCenter = playerCollider.bounds.center;

            if(contactPoint.y - playerCenter.y  < -0.7f)
            {
                EnemyDeath();
            }
            else
            {
                HurtPlayer(collision.gameObject);
            }

        }
    }

    protected virtual void EnemyDeath()
    {

    }

    protected virtual void HurtPlayer(GameObject Player)
    {
        Player.GetComponent<PlayerMovement>().Kill();
    }
}
