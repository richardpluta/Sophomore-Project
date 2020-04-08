using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateController : MonoBehaviour
{
    [SerializeField] private int AmmoAmount;
    private bool Enabled;
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;

    void Start()
    {
        Enabled = true;
        Collider = gameObject.GetComponent<BoxCollider2D>();
        Renderer = gameObject.GetComponent<SpriteRenderer>();
 
        if (Collider == null || Renderer == null)
        {
            Enabled = false;
            Debug.LogError("AmmoCrate " + gameObject.name + " is missing a BoxCollider2D or SpriteRenderer");
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        WeaponController weapon = hit.GetComponent<WeaponController>();
        if (weapon != null)
        {
            StatsController.AmmoPickups++;
            Collider.enabled = false;
            Renderer.enabled = false;
            weapon.RefillAmmo(AmmoAmount);
        }
    }
}
