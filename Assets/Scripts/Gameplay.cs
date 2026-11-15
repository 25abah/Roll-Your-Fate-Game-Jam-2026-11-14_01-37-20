using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public float health;
    public int currency;
    public int counter = 0;
    [SerializeField] private EnemyBase[] enemies;
    [SerializeField] private Manager manager;

    void Start()
    {
        health = 100f;
        currency = 0;
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }

    void FixedUpdate()
    {
        if (manager.currentGameState != GameState.GameOver)
        {
            counter++;
            SpawnEnemy(5f);
        } 
        else
        {
            GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enem in allEnemies)
            {
                Destroy(enem);
            }
        }
    }

    void SpawnEnemy(float spawnTime){
        if (counter == 50*spawnTime)
        {
            int enemyNum = Random.Range(1,1000);
            Debug.Log("Random Num: " + enemyNum);
            for(int i = 0; i < enemies.Length; i++)
            {
                Debug.Log("Enemy Spawn Rate" + enemies[i].getSpawnRate());
                if(enemies[i].getSpawnRate() > enemyNum)
                {
                    Instantiate(enemies[i], new Vector3(-8f, 4.4f, 0), Quaternion.identity);
                    break;
                }
            }
            counter = 0; 
        }   
    }
}
