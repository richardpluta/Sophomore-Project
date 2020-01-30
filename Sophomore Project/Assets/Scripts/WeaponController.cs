using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform FirePoint;
    [SerializeField] private LineRenderer Beam;
    [SerializeField] private int Damage;
    [SerializeField] private int MaxAmmo;
    private int CurrentAmmo;
    

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmo = MaxAmmo;
        Debug.Log("WeaponController starting");       
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetButtonDown("Fire1") && CurrentAmmo > 0)
        {
            StartCoroutine(Shoot());
        }
    }

    public void RefillAmmo(int amount)
    {
        CurrentAmmo = CurrentAmmo + amount <= MaxAmmo ?  CurrentAmmo + amount : MaxAmmo;
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, FirePoint.right);
        
        if (hit)
        {
            Beam.SetPosition(0, FirePoint.position);
            Beam.SetPosition(1, hit.point);
        } else
        {
            Beam.SetPosition(0, FirePoint.position);
            Beam.SetPosition(1, FirePoint.position + FirePoint.right * 100);
        }

        --CurrentAmmo;
        Beam.enabled = true;
        yield return new WaitForSeconds(0.02f);
        Beam.enabled = false;
    }
}
