using UnityEngine;

public class TowerSpeaker : TowerBase
{
    public override void SetVariables()
    {
        price = 15;
        damage = 3;
        range = 6;
        name = "Speaker";
        description = "AOE tower that hits enemies all around it.";
    }

    protected override void ShootTarget()
    {
        foreach (GameObject target in targets)
        {
            target.GetComponent<EnemyBase>().TakeDamage(damage);
        }
    }
}
