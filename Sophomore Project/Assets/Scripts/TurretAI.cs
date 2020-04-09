using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public float distance;
    public float wakeRange;
    public float shotInterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    public bool awake = false;
    public bool lookingRight = true;

    public GameObject bullet;
    public Transform target;
    public Transform shootPointRight;
    public Transform shootPointLeft;

    void Awake()
    {

    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        RangeCheck();

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }

        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakeRange)
        {
            awake = true;
        }

        if (distance > wakeRange)
        {
            awake = false;
        }


    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;

        if (bulletTimer >= shotInterval)
        {
            Vector2 direction = target.transform.position - transform.position;

            

            if (!attackingRight)
            {
                SoundManagerScript.PlaySound("TurretFire");
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }

            if (attackingRight)
            {
                SoundManagerScript.PlaySound("TurretFire");
                GameObject bulletClone;
                bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
                bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

                bulletTimer = 0;
            }
        }
    }
}
