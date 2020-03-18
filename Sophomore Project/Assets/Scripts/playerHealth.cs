using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class playerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int EnemyDamage;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == "Enemy")
        {
            damagePlayer(1);
        }
    }

    public void damagePlayer(int dmg)
    {
        health -= dmg;
    }
}
