using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCanisterController : MonoBehaviour
{
    [SerializeField] private int HeartAmmount;
    private bool Enabled;
    private PolygonCollider2D Collider;
    private SpriteRenderer Renderer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HeartCanisterContoller starting!");
        Enabled = true;
        Collider = gameObject.GetComponent<PolygonCollider2D>();
        Renderer = gameObject.GetComponent<SpriteRenderer>();
 
        if (Collider == null || Renderer == null)
        {
            Enabled = false;
            Debug.LogError("HeartCanister " + gameObject.name + " is missing a PolygonCollider2D or SpriteRenderer");
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        playerHealth playerHeartsController = hit.GetComponent<playerHealth>();
        if (playerHeartsController != null)
        {
            StatsController.HeartPickups++;
            Collider.enabled = false;
            Renderer.enabled = false;
            playerHeartsController.healPlayer(HeartAmmount);
        }
    }
}
