using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip characterDeath, characterHurt, characterJump, enemyAttack, enemyJump, enemyDie, enemyHurt, enemySeesCharacter, laser, menuSelect, pause, ammoPickup, heartPickup, turretFire;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        characterDeath = Resources.Load<AudioClip>("CharacterDie");
        characterHurt = Resources.Load<AudioClip>("CharacterHurt");
        characterJump = Resources.Load<AudioClip>("CharacterJump");
        enemyAttack = Resources.Load<AudioClip>("EnemyAttack");
        enemyJump = Resources.Load<AudioClip>("EnemyJump");
        enemyDie = Resources.Load<AudioClip>("EnemyDie");
        enemyHurt = Resources.Load<AudioClip>("EnemyHurt");
        enemySeesCharacter = Resources.Load<AudioClip>("EnemySeesCharacter");
        laser = Resources.Load<AudioClip>("LaserShot");
        menuSelect = Resources.Load<AudioClip>("MenuSelect");
        pause = Resources.Load<AudioClip>("Pause");
        ammoPickup = Resources.Load<AudioClip>("AmmoPickup");
        heartPickup = Resources.Load<AudioClip>("HeartPickup");
        turretFire = Resources.Load<AudioClip>("TurretFire");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "CharacterDie":
                audioSrc.PlayOneShot(characterDeath);
                break;
            case "CharacterHurt":
                audioSrc.PlayOneShot(characterHurt);
                break;
            case "CharacterJump":
                audioSrc.PlayOneShot(characterJump);
                break;
            case "EnemyAttack":
                audioSrc.PlayOneShot(enemyAttack);
                break;
            case "EnemyJump":
                audioSrc.PlayOneShot(enemyJump);
                break;
            case "EnemyDie":
                audioSrc.PlayOneShot(enemyDie);
                break;
            case "EnemyHurt":
                audioSrc.PlayOneShot(enemyHurt);
                break;
            case "EnemySeesCharacter":
                audioSrc.PlayOneShot(enemySeesCharacter);
                break;
            case "LaserShot":
                audioSrc.PlayOneShot(laser);
                break;
            case "MenuSelect":
                audioSrc.PlayOneShot(menuSelect);
                break;
            case "Pause":
                audioSrc.PlayOneShot(pause);
                break;
            case "AmmoPickup":
                audioSrc.PlayOneShot(ammoPickup);
                break;
            case "HeartPickup":
                audioSrc.PlayOneShot(heartPickup);
                break;
            case "TurretFire":
                audioSrc.PlayOneShot(turretFire);
                break;
        }
    }
}
