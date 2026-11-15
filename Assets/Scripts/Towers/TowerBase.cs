using UnityEngine;
using System.Collections.Generic;

public abstract class TowerBase : MonoBehaviour
{
    public int price { get; protected set; }
    public float range{ get; protected set; }
    public int damage{ get; protected set; }
    public string name{ get; protected set; }
    public string description{ get; protected set; }
    protected TetherBase lastTether = null;
    protected TetherBase nextTether = null;
    protected CircleCollider2D targetArea;
    protected List<GameObject> targets = new List<GameObject>();
    private const float cooldownShot = 0.5f;
    private float cooldown = 0f;
    bool shooting = false;
    protected int fiberOpticsAmount = 0;
    [SerializeField] GameObject projectile;
    [SerializeField] private Sprite projectileSprite;
    protected bool hasShot = false;
    public Animator anim;
     
    [SerializeField] private float maxDistance = 5f;

    void Awake()
    {
        anim = GetComponent<Animator>();
        SetVariables();
        targetArea = GetComponent<CircleCollider2D>();
        targetArea.radius = range;
        
    }

    void FixedUpdate()
    {
        if (hasShot)
        {
            anim.SetBool("isAttacking", false);
            hasShot = false;
        }
        if (targets.Count > 0 && cooldown >= cooldownShot)
        {
            ShootTarget();
            cooldown -= cooldownShot;
        }
        if (shooting)
        {
            cooldown += Time.fixedDeltaTime;
            if (targets.Count == 0)
            {
                shooting = false;
            }
        }
    }

    /// <summary>
    /// set price, range, and damage variables for the tower.
    /// </summary>
    public abstract void SetVariables();

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
        nextTether.FiberOptic(fiberOpticsAmount, false);
        return newTether;
    }

    public void GetLastTether(TetherBase tether)
    {
        lastTether = tether;
    }

    public virtual void Break()
    {
        if (nextTether != null)
        {
            nextTether.Break();
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            targets.Add(collision.gameObject);
            shooting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (GameObject target in targets)
        {
            if (collision.gameObject == target)
            {
                targets.Remove(collision.gameObject);
                return;
            }
        }
    }

    protected virtual void ShootTarget()
    {
        anim.SetBool("isAttacking", true);
        hasShot = true;
        Vector3 projectilePath = targets[0].transform.position - transform.position;
        GameObject newProjectile = GameObject.Instantiate<GameObject>(projectile, transform.position, Quaternion.identity);
        float projectileAngle;
        if (projectilePath.y < 0)
        {
            projectileAngle = 90 - Vector3.Angle(projectilePath, Vector3.right);
        }
        else if (projectilePath.y > 0)
        {
            projectileAngle = 90 + Vector3.Angle(projectilePath, Vector3.right);
        }
        else
        {
            projectileAngle = 90;
        }
        newProjectile.transform.Rotate(Vector3.forward, projectileAngle);
        newProjectile.GetComponent<Projectile>().SetDamage(damage);
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = projectilePath * 2;
        if (projectileSprite != null)
        {
            newProjectile.GetComponent<SpriteRenderer>().sprite = projectileSprite;
        }

        //targets[0].GetComponent<EnemyBase>().TakeDamage(damage);
        Debug.Log("FIRE!");
        if (targets.Count == 0)
        {
            shooting = false;
        } 
    }

    public void FiberOptics(int amount, bool back)
    {
        if (amount > fiberOpticsAmount || amount == 0)
        {
            fiberOpticsAmount = amount;
        }
        if (back && lastTether != null)
        {
            lastTether.FiberOptic(fiberOpticsAmount, back);
        }
        else if (nextTether != null)
        {
            nextTether.FiberOptic(fiberOpticsAmount, back);
        }
    }
}
