using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHand : MonoBehaviour
{
    public GameObject player = null;

    void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 dist = playerPos - transform.position;
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.rotation = Quaternion.LookRotation(dist, transform.forward);
    }
}
