using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrateController : MonoBehaviour
{
    [SerializeField] private int AmmoAmount;
    private bool Enabled;
    private BoxCollider2D Collider;
    private SpriteRenderer Renderer;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        WeaponController weapon = hit.GetComponent<WeaponController>();
        if (weapon != null)
        {
            Collider.enabled = false;
            Renderer.enabled = false;
            weapon.RefillAmmo(AmmoAmount);
        }
    }
}
