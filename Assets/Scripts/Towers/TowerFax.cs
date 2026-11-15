using UnityEngine;

public class TowerFax : TowerBase
{
    public override void SetVariables()
    {
        price = 30;
        range = 10;
        damage = 10;
        fiberOpticsAmount = 0;
        name = "Fax";
        description = "Long range sniper that shoots cease and desists at enemies";
    }
}
