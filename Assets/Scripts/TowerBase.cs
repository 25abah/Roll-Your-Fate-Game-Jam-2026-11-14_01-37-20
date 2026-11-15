using UnityEngine;
using System.Collections.Generic;

public abstract class TowerBase : MonoBehaviour
{
    protected int price;
    protected float range;
    protected int damage;
    protected TetherBase lastTether = null;
    protected TetherBase nextTether = null;
    protected CircleCollider2D targetArea;
    private List<GameObject> targets = new List<GameObject>();
    private const float cooldownShot = 0.5f;
    private float cooldown = 0f;

    [SerializeField] private float maxDistance = 5f;

    void Awake()
    {
        SetVariables();
        targetArea = GetComponent<CircleCollider2D>();
        targetArea.radius = range;
    }

    void FixedUpdate()
    {
        if (targets.Count > 0 && cooldown >= cooldownShot)
        {
            ShootTarget();
            cooldown -= cooldownShot;
        }
        cooldown += Time.fixedDeltaTime;
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
            tetherVec = Vector3.ClampMagnitude(tetherVec, maxDistance);
        }

        Vector3 midPoint = transform.position + tetherVec / 2;
        float tetherAngle;

        if (tetherVec.y < 0)
        {
            tetherAngle = -Vector3.Angle(tetherVec, Vector3.right);
        }
        else if (tetherVec.y > 0)
        {
            tetherAngle = Vector3.Angle(tetherVec, Vector3.right);
        }
        else
        {
            tetherAngle = 0;
        }

        GameObject newTether = Instantiate<GameObject>(tether, midPoint, Quaternion.identity);
        Vector3 setWidth = Vector3.one;
        setWidth.x = tetherVec.magnitude;
        setWidth.y = .25f;
        newTether.transform.localScale = setWidth;
        newTether.transform.Rotate(Vector3.forward, tetherAngle);
        nextTether = newTether.GetComponent<TetherBase>();
        nextTether.GetLastTower(tetherVec, this);
        return newTether;
    }

    public void GetLastTether(TetherBase tether)
    {
        lastTether = tether;
    }

    public void Break()
    {
        if (nextTether != null)
        {
            nextTether.Break();
        }
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject target in targets)
        {
            if (collision.gameObject == target)
            {
                targets.Remove(collision.gameObject);
            }
        }
    }

    private void ShootTarget()
    {
        targets[0].GetComponent<EnemyBase>().TakeDamage(damage);
    }
}
