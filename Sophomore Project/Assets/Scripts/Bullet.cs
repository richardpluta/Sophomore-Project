using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
 void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger != true)
        {
            if (col.CompareTag("Player"))
            {
                col.GetComponent<playerHealth>().damagePlayer(1);
            }

            Destroy(gameObject);
        }
    }
}
