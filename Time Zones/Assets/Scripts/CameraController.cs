using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offs = new Vector3(0f, 0f, -5f);
    public float timeSmooth = 0.25f;
    public GameObject target;

    private Vector3 speed = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.transform.position + offs;
        transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref speed, timeSmooth);
    }
}
