using UnityEngine;

public abstract class TowerBase : MonoBehaviour
{
    protected int price;
    protected float range;
    protected float damage;
    protected TetherBase lastTether = null;
    protected TetherBase nextTether = null;

    [SerializeField] private float maxDistance = 5f;

    void Awake()
    {
        SetVariables();
    }

    /// <summary>
    /// set price, range, and damage variables for the tower.
    /// </summary>
    protected abstract void SetVariables();

    public GameObject SetNextTether(Vector3 position, GameObject tether)
    {
        Vector3 tetherVec = position - transform.position;
        if (tetherVec.magnitude > maxDistance)
        {
            Vector3.ClampMagnitude(tetherVec, maxDistance);
        }

        Vector3 midPoint = transform.position + tetherVec / 2;
        float tetherAngle;

        if (tetherVec.x < 0)
        {
            tetherAngle = Vector3.Angle(tetherVec, Vector3.left);
        }
        else if (tetherVec.x > 0)
        {
            tetherAngle = Vector3.Angle(tetherVec, Vector3.right);
        }
        else
        {
            tetherAngle = 90;
        }

        GameObject newTether = Instantiate<GameObject>(tether, midPoint, Quaternion.identity);
        newTether.transform.localScale = tetherVec;
        newTether.transform.Rotate(Vector3.forward, tetherAngle);
        nextTether = newTether.GetComponent<TetherBase>();
        nextTether.GetLastTower(tetherVec, this);
        return newTether;
    }

    public void GetLastTether(TetherBase tether)
    {
        lastTether = tether;
    }
}
