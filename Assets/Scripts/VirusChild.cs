using UnityEngine;

public class VirusChild : EnemyBase
{
    void Awake()
    {
        health = 10;
        damage = 5;
        spawnRate = 950;
        speed = 1f;
    }
}
