using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceFly : MonoBehaviour
{
    
    public float speed; //Speed of vertical motion of the furnaceFly
    public float attackCooldown; //Cooldown period of the projectile attack
    public GameObject target; //Reference to the target to be attacked
    public GameObject projectile; //Reference to the projectile prefab which the furnaceFly will shoot
    public float projectileSpeed; //Speed of the projectile 
    public float aggroRange; //Clear name

    private bool movingUp; //State used for vertical motion
    private GameObject projectileClone; //Reference to work with each spawned projectile
    private float localAttackCooldown = 0; // Local variable to use for cooldown of the attack
    private Transform projectileSpawnLocation; //Adjustable child object to determine where the projectile is spawned from
    private float upperBound; //Adjustable child object to determine how far up the furnaceFly goes
    private float lowerBound; //Adjustable child object to determine how far down the furnaceFly goes
    private Rigidbody2D projectileRGB; //Reference to be used to store the rigidbody component of the projectile when it is spawned.

    void Start()
    {
        if(target == null)
        target = GameObject.FindGameObjectWithTag("Player");
        projectileSpawnLocation = gameObject.transform.GetChild(0).transform;
        upperBound = transform.Find("UpperBound").position.y;
        lowerBound = transform.Find("LowerBound").position.y;
    }

    
    void Update()
    {
        

        //Flipping furnacefly according to where the target is
        if (target.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        //Using update frames to run the cooldown of the projectile attack
        if (localAttackCooldown > 0)
            localAttackCooldown -= Time.deltaTime;
        else   //Launch projectile
        {
            projectileClone = GameObject.Instantiate(projectile, projectileSpawnLocation.position, transform.rotation);
            localAttackCooldown = attackCooldown;
            Debug.Log("This is the rigidbody velocity" + projectileClone.GetComponent<Rigidbody2D>().velocity);
            projectileRGB = projectileClone.GetComponent<Rigidbody2D>();
            projectileRGB.velocity = (target.transform.position - transform.position).normalized * projectileSpeed;
            float angle = Mathf.Atan2(projectileRGB.velocity.y, projectileRGB.velocity.x) * Mathf.Rad2Deg;
            projectileClone.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Debug.Log("Cooldown Reset");
        }

        //Simple state system to switch between upward and downward motion
        if (movingUp)
        {
            if (transform.position.y <= upperBound)
                transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
            else
                movingUp = !movingUp;

        }
        else
        {
            if (transform.position.y >= lowerBound)
                transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime);
            else
                movingUp = !movingUp;
        }
    }
}
