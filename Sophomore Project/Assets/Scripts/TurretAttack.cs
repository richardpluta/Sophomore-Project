using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{

    public TurretAI turretAI;

    public bool isLeft = false;

    void Awake()
    {
        turretAI = gameObject.GetComponentInParent<TurretAI>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (isLeft)
            {
                turretAI.Attack(false);
            }
            else
            {
                turretAI.Attack(true);
            }
        }
    }
}
