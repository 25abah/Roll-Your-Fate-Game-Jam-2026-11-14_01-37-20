using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    protected int price;
    protected float range;
    protected float damage;
    protected TetherBase lastTether = null;
    protected TetherBase nextTether = null;

    void Awake()
    {
        SetVariables();
    }

    /// <summary>
    /// set price, range, and damage variables for the tower.
    /// </summary>
    protected abstract void SetVariables();
}
