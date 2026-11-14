using UnityEngine;

public class HackChild : EnemyBase
{
    void Awake()
    {
        health = 30;
        damage = 10;
        spawnRate = 995;
        speed = 1.5f;
    }
    
}
