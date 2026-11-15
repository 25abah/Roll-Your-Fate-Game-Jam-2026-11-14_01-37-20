using UnityEngine;

public class TetherBase : MonoBehaviour
{
    TowerBase lastTower;
    TowerBase nextTower;
    Vector3 tetherVec;
    protected int fiberOpticsAmount;

    public void GetLastTower(Vector3 tetherVec, TowerBase tower)
    {
        lastTower = tower;
        this.tetherVec = tetherVec;
    }

    public GameObject SetNextTower(GameObject tower)
    {
        Vector3 towerPosition = transform.position + tetherVec / 2;
        GameObject newTower = GameObject.Instantiate<GameObject>(tower, towerPosition, Quaternion.identity);
        nextTower = newTower.GetComponent<TowerBase>();
        nextTower.GetLastTether(this);
        nextTower.FiberOptics(fiberOpticsAmount, false);
        return newTower;
    }

    public void Break()
    {
        if (nextTower != null)
        {
            nextTower.Break();
        }
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.Break();
        }
    }

    public void FiberOptic(int boostAmount, bool back)
    {
        int actualAmount = (int)Mathf.Floor(boostAmount / 2);
        if (back)
        {
            lastTower.FiberOptics(actualAmount, back);
        }
        else if (nextTower != null)
        {
            nextTower.FiberOptics(actualAmount, back);
        }
    }
}
