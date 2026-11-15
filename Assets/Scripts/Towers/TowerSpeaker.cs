using UnityEngine;

public class TowerSpeaker : TowerBase
{
    protected override void SetVariables()
    {
        price = 15;
        damage = 3;
        range = 6;
    }

    protected override void ShootTarget()
    {
        foreach (GameObject target in targets)
        {
            target.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }
}
