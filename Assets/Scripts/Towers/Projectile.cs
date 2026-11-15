using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int damage;
    private const float despawn = 2;
    private float despawnTimer = 0;

    private void FixedUpdate()
    {
        despawnTimer += Time.fixedDeltaTime;
        if (despawnTimer >= despawn)
        {
            Destroy(gameObject);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
