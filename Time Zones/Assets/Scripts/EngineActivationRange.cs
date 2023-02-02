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
        if(collision.tag == "Player")
        {
            isPlayerActivating = true;
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
