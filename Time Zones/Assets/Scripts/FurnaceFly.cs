using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceFly : MonoBehaviour
{
    public float yMax;
    public float yMin;
    public float speed;
    public float attackCooldown;
    public GameObject target;
    public GameObject projectile;
    public float projectileSpeed;
    public float aggroRange;

    private bool movingUp;
    private GameObject projectileClone;
    private float localAttackCooldown = 0;
    private Transform projectileSpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        target = GameObject.FindGameObjectWithTag("Player");
        projectileSpawnLocation = gameObject.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (localAttackCooldown > 0)
            localAttackCooldown -= Time.deltaTime;
        else
        {
            projectileClone = GameObject.Instantiate(projectile, projectileSpawnLocation.position, transform.rotation);
            localAttackCooldown = attackCooldown;
            Debug.Log("This is the rigidbody velocity" + projectileClone.GetComponent<Rigidbody2D>().velocity);
            projectileClone.GetComponent<Rigidbody2D>().velocity = (target.transform.position - transform.position).normalized * projectileSpeed;
            
            Debug.Log("Cooldown Reset");
        }

        if (movingUp)
        {
            if (transform.position.y <= yMax)
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
            else
                movingUp = !movingUp;

        }
        else
        {
            if (transform.position.y >= yMin)
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
            else
                movingUp = !movingUp;
        }
    }
}
