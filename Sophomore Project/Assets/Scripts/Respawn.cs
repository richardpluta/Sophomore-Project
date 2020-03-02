/*
    Rafael Montes
    2/10/2020
    This code will make the player respawn if he/she touches a killzone.
*/
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject respawnPoint;
    public GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }
}
