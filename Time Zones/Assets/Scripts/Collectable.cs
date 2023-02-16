using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private GameObject targetEngine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            targetEngine.GetComponent<Engine>().EnableEngine();
           // GameObject.Collected()
        }
    }
}
