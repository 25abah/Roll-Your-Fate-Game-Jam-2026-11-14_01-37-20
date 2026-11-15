using UnityEngine;

public class TowerFiberOptics : TowerBase
{
    public override void SetVariables()
    {
        price = 20;
        damage = 0;
        range = 0;
        fiberOpticsAmount = 8;
        if (lastTether != null)
        {
            lastTether.FiberOptic(fiberOpticsAmount, true);
        }
        name = "Fiber Optics";
        description = "Support tower that boosts the damage of other towers.";
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
