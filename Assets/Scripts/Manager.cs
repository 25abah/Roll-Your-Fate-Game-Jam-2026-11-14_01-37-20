using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    SetupPhase,
    WavePhase,
    GameOver
}
public class Manager : MonoBehaviour
{
    public GameObject[] towers;
    public GameObject[] tethers;
    GameObject activeTower;
    GameObject activeTether;
    int activeTowerIndex = 0;
    int activeTetherIndex = 0;
    public GameState currentGameState = GameState.Menu;
    Gameplay gameplay;
    GameObject currentObject = null;
    float timer = 20f;
    float timerWave = 60f;
    [SerializeField] private UIManager uiManager;
    private int waveCount = 0;
    void Start()
    {
        activeTower = towers[activeTowerIndex];
        activeTether = tethers[activeTetherIndex];
        gameplay = GetComponent<Gameplay>();
        uiManager = GameObject.Find("Full UI").GetComponent<UIManager>();

    }

    void Update()
    {
        if (currentGameState == GameState.Menu)
        {
            uiManager.enableTitleScreen();
            if (Input.GetMouseButtonDown(2))
            {
                uiManager.disableTitleScreen();
                currentGameState = GameState.SetupPhase;
                waveCount++;
            }
        }
        else if (currentGameState == GameState.SetupPhase || currentGameState == GameState.WavePhase)
        {
            if(currentGameState == GameState.SetupPhase)
            {
                uiManager.updateTimer(timer);
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    currentGameState = GameState.WavePhase;
                    timer = 0.0f;
                    timerWave = 60f;
                }
            }
            else
            {
                uiManager.updateTimer(timerWave);
                timerWave -= Time.deltaTime;
                if (timerWave <= 0)
                {
                    currentGameState = GameState.SetupPhase;
                    waveCount++;
                    timerWave = 0.0f;
                    timer = 20f;
                } 
            }
            if (currentObject == null || !currentObject.CompareTag("Tether"))
            {
                ScrollTowers();
            }
            else
            {
                ScrollTethers();
            }
            SpawnTower();
        }
        if (gameplay.TakeDamage(0) <= 0)
        {
            currentGameState = GameState.GameOver;
            uiManager.enableGameOver();
        }
        if (currentGameState == GameState.GameOver)
        {
            if (Input.GetMouseButtonDown(2))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void ScrollTowers()
    {
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelValue > 0)
        {
            activeTowerIndex = (activeTowerIndex - 1 + towers.Length) % towers.Length;
            activeTower = towers[activeTowerIndex];
            uiManager.previousTower();
            Debug.Log("Active Tower: " + activeTower.name);
        }  
        if (scrollWheelValue < 0)
        {
            activeTowerIndex = (activeTowerIndex + 1 + towers.Length) % towers.Length;
            activeTower = towers[activeTowerIndex];
            uiManager.nextTower();
            Debug.Log("Active Tower: " + activeTower.name);
        }
    }

    void ScrollTethers()
    {
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelValue > 0)
        {
            activeTetherIndex = (activeTetherIndex - 1 + tethers.Length) % tethers.Length;
            activeTether = tethers[activeTetherIndex];
            Debug.Log("Active Tether: " + activeTether.name);
        }
        if (scrollWheelValue < 0)
        {
            activeTetherIndex = (activeTetherIndex + 1 + tethers.Length) % tethers.Length;
            activeTether = tethers[activeTetherIndex];
            Debug.Log("Active Tether: " + activeTether.name);
        }
    }

    void SpawnTower(){
        if (Input.GetMouseButtonDown(2))
        {
            if (currentObject == null)
            {
                currentObject = Instantiate(activeTower, FindMousePosition(), Quaternion.identity);
                activeTower.transform.position = new Vector3(activeTower.transform.position.x, activeTower.transform.position.y, 0);
            }
            else if (currentObject.CompareTag("Tower"))
            {
                currentObject = currentObject.GetComponent<TowerBase>().SetNextTether(FindMousePosition(), activeTether);
                activeTether.transform.position = new Vector3(activeTether.transform.position.x, activeTether.transform.position.y, 0);
            }
            else
            {
                currentObject = currentObject.GetComponent<TetherBase>().SetNextTower(activeTower);
                activeTether.transform.position = new Vector3(activeTether.transform.position.x, activeTether.transform.position.y, 0);
            }
        }
    }

    Vector3 FindMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z); 
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log("Mouse World Position: " + worldPos);
        return worldPos;
    }
}
