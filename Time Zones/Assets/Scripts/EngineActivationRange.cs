using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineActivationRange : MonoBehaviour
{
    #region Fields
    public bool isPlayerActivating = false;
    #endregion Fields

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //if player is within range
        if(collision.tag == "Player")
        {
            //and they are grounded
            if(collision.gameObject.GetComponent<PlayerMovement>().IsGrounded())
            {
                isPlayerActivating = true;
            }
            else
            {
                isPlayerActivating = false;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isPlayerActivating = false;
        }
    }
}
