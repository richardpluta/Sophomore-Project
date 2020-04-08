using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class StatsController
{
    static public int Deaths;
    static public int EnemiesKilled;
    static public int ShotsFired;
    static public int ShotsHit;
    static public int HeartPickups;
    static public int AmmoPickups;
    static public float Playtime;

    static public float GetAccuracy()
    {
        return ShotsFired > 0 ? (float)ShotsHit / ShotsFired : 0f;
    }
}
