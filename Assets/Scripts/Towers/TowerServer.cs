using UnityEngine;

public class TowerServer : TowerBase
{
    [SerializeField] Gameplay gameplay;
    private const float coolDown = 2;
    private float coolDownCount = 0;
    public override void SetVariables()
    {
        price = 50;
        damage = 0;
        range = 0;
        name = "Server";
        description = "Support tower that grants bits over time.";
    }

    private void FixedUpdate()
    {
        if (coolDownCount >= coolDown)
        {
            gameplay.addCurrency(10);
            coolDownCount -= coolDown;
        }
        coolDownCount += Time.fixedDeltaTime;
    }
}
