using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform FirePoint;
    [SerializeField] private LineRenderer Beam;
    [SerializeField] private int Damage;
    [SerializeField] private int MaxAmmo;
    private int CurrentAmmo;
    public Text ammoDisplay;

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmmo = MaxAmmo;
        Debug.Log("WeaponController starting");       

        // ammoDisplay = GameObject.FindObjectOfType<Text>();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetButtonDown("Fire1") && CurrentAmmo > 0)
        {
            SoundManagerScript.PlaySound("LaserShot");
            StartCoroutine(Shoot());
        }
        
        ammoDisplay.text = CurrentAmmo.ToString();
    }

    public void RefillAmmo(int amount)
    {
        CurrentAmmo = CurrentAmmo + amount <= MaxAmmo ?  CurrentAmmo + amount : MaxAmmo;
    }

    IEnumerator Shoot()
    {
        StatsController.ShotsFired++;
        RaycastHit2D hit = Physics2D.Raycast(FirePoint.position, FirePoint.right, 100f, LayerMask.GetMask("Ground","Enemy"));
        
        if (hit)
        {
            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if (enemy != null)
            {
                StatsController.ShotsHit++;
                enemy.DamageSelf(Damage);
            }
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
