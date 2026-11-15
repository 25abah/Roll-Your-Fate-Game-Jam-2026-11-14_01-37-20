using UnityEngine;

public class TowerFiberOptics : TowerBase
{
    protected override void SetVariables()
    {
        price = 20;
        damage = 0;
        range = 0;
        fiberOpticsAmount = 8;
        lastTether.FiberOptic(fiberOpticsAmount, true);
    }

    public override void Break()
    {
        lastTether.FiberOptic(0, true);
        if (nextTether != null)
        {
            nextTether.FiberOptic(0, false);
        }
        base.Break();
    }
}
