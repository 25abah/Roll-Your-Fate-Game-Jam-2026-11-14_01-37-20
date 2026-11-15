using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private int currency;
    [SerializeField] private int counter = 0;
    [SerializeField] private EnemyBase[] enemies;
    [SerializeField] private Manager manager;
    [SerializeField] private UIManager uiManager;

    public int addCurrency(int money)
    {
        currency += money;
        return currency;
    }

    void Start()
    {
        health = 100f;
        currency = 0;
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        uiManager = GameObject.Find("Full UI").GetComponent<UIManager>();
    }

    void FixedUpdate()
    {
        if (manager.currentGameState == GameState.WavePhase)
        {
            counter++;
            SpawnEnemy(5f);
            uiManager.updateCurrency(currency);
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

    public float TakeDamage(int damage)
    {
        health -= damage;
        uiManager.updatehpBar(health/100);
        return health;
    }

    void SpawnEnemy(float spawnTime)
    {
        if (counter == 50*spawnTime)
        {
            int enemyNum = Random.Range(1,1000);
            Debug.Log("Random Num: " + enemyNum);
            for(int i = 0; i < enemies.Length; i++)
            {
                Debug.Log("Enemy Spawn Rate" + enemies[i].getSpawnRate());
                if(enemies[i].getSpawnRate() > enemyNum)
                {
                    EnemyBase newEnemy = Instantiate(enemies[i], new Vector3(-8f, 4.4f, 0), Quaternion.identity);
                    newEnemy.GetStronger(manager.waveCount);
                    Debug.Log(newEnemy.health);
                    break;
                }
            }
            counter = 0; 
        }   
    }
}
