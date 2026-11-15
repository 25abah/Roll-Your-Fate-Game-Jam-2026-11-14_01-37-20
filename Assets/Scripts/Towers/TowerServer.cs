using UnityEngine;

public class TowerServer : TowerBase
{
    private const float coolDown = 2;
    private float coolDownCount = 0;
    protected override void SetVariables()
    {
        price = 50;
        damage = 0;
        range = 0;
    }

    private void FixedUpdate()
    {
        if (coolDownCount >= coolDown)
        {
            //TODO: increase money
            coolDownCount -= coolDown;
        }
        coolDownCount += Time.fixedDeltaTime;
    }
}
