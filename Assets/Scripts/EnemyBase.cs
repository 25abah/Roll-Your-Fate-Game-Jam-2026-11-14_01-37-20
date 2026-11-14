using UnityEngine;
using System.Collections.Generic;

public class EnemyBase : MonoBehaviour
{
    public float health;
    public int damage;
    public int spawnRate;
    public float speed;
    [SerializeField] private Gameplay gameplay;

    void Start()
    {
        gameplay = GameObject.Find("GameManager").GetComponent<Gameplay>();
    }

    void FixedUpdate()
    {   
        if (gameObject != null)
        {
            PathFinding(gameObject);
            if (gameObject.transform.position.y < -4.5f)
            {
                gameplay.health -= damage;
                Destroy(gameObject);
            }
        }
    }

    protected void PathFinding(GameObject enemy)
    {
        if (enemy.transform.position.y > -4.4f && enemy.transform.position.x < 0)
        {
            //Debug.Log("Moving Down on left");
            enemy.transform.position += new Vector3(0, -(speed * Time.deltaTime), 0);
        }
        else if (enemy.transform.position.y <= -4.4f && enemy.transform.position.x <= 0)
        {
            //Debug.Log("Moving Right on bottom");
            enemy.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Mathf.Round(enemy.transform.position.x) == 0 && enemy.transform.position.y <= 4.4f)
        {
            //Debug.Log("Moving Up on middle");
            enemy.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else if (enemy.transform.position.y >= 4.4f && enemy.transform.position.x > 0 && enemy.transform.position.x < 8)
        {
            //Debug.Log("Moving Right on top");
            enemy.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Mathf.Round(enemy.transform.position.x) == 8)
        {
            //Debug.Log("Moving Down on right");
            enemy.transform.position += new Vector3(0, -(speed * Time.deltaTime), 0);
        }
        else{
            Debug.Log("There is an error in the pathfinding algorithm");
        }
    }
}
