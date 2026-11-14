using UnityEngine;
using UnityEngine.Rendering;

public class TowerGenericTest : TowerBase
{
    protected override void SetVariables()
    {
        price = 5;
        range = 10;
        damage = 3;
    }
}
