using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public float health;
    public int currency;
    public int counter = 0;
    public GameObject enemyPrefab;

    void Start()
    {
        health = 100f;
        currency = 0;
    }

    void FixedUpdate()
    {
        counter++;
        SpawnEnemy(5f);
    }

    void SpawnEnemy(float spawnTime){
        if (counter == 50*spawnTime)
        {
            Instantiate(enemyPrefab, new Vector3(-8f, 4.4f, 0), Quaternion.identity);
            counter = 0;
        }   
        Debug.Log("Counter: " + counter);
    }
}
