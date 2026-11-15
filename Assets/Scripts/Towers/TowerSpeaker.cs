using UnityEngine;

public class TowerSpeaker : TowerBase
{
    public override void SetVariables()
    {
        price = 15;
        damage = 3;
        range = 6;
        fiberOpticsAmount = 0;
        name = "Speaker";
        description = "AOE tower that hits enemies all around it.";
    }

    protected override void ShootTarget()
    {   anim.SetBool("isAttacking", true);
        hasShot = true;
        foreach (GameObject target in targets)
        {
            
            target.GetComponent<EnemyBase>().TakeDamage(realDamage);
        }
    }
}
