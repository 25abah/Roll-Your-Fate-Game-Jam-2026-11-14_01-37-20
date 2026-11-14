using UnityEngine;

public class TetherBase : MonoBehaviour
{
    TowerBase lastTower;
    TowerBase nextTower;
    Vector3 tetherVec;

    public void GetLastTower(Vector3 tetherVec, TowerBase tower)
    {
        lastTower = tower;
        this.tetherVec = tetherVec;
    }

    public void SetNextTower(GameObject tower)
    {
        Vector3 towerPosition = transform.position + tetherVec / 2;
        GameObject newTower = GameObject.Instantiate<GameObject>(tower, towerPosition, Quaternion.identity);
        nextTower = newTower.GetComponent<TowerBase>();
        nextTower.GetLastTether(this);
    }
}
