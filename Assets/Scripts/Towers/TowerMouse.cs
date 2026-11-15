using UnityEngine;

public class TowerMouse : TowerBase
{
    public override void SetVariables()
    {
        price = 10;
        range = 3;
        damage = 5;
        name = "Mouse";
        description = "Short ranged tower.";
    }
}
