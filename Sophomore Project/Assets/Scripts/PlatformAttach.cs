using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttach : MonoBehaviour
{
   public GameObject player;

   private void OnTriggerEnter(CircleCollider2D collider)
	{
		if (collider.gameObject == player)
		{
			player.transform.parent = transform;
		}
	}

	private void OnTriggerExit(CircleCollider2D collider)
	{
		if (collider.gameObject == player)
		{
			player.transform.parent = null;
		}
	}
}
