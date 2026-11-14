using UnityEngine;
public enum GameState
{
    Menu,
    SetupPhase,
    WavePhase,
    GameOver
}
public class ScrollWheelManager : MonoBehaviour
{
    public GameObject[] towers;
    public GameObject[] tethers;
    GameObject activeTower;
    GameObject activeTether;
    int activeTowerIndex = 0;
    int activeTetherIndex = 0;
    public GameState currentGameState = GameState.SetupPhase;
    Gameplay gameplay;
    GameObject currentObject = null;
    void Start()
    {
        activeTower = towers[activeTowerIndex];
        activeTether = tethers[activeTetherIndex];
        gameplay = GetComponent<Gameplay>();
    }

    void Update()
    {
        if (currentGameState == GameState.SetupPhase || currentGameState == GameState.WavePhase)
        {
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
        if (gameplay.health <= 0)
        {
            currentGameState = GameState.GameOver;
        }
    }

    void ScrollTowers()
    {
        float scrollWheelValue = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelValue > 0)
        {
            activeTowerIndex = (activeTowerIndex - 1 + towers.Length) % towers.Length;
            activeTower = towers[activeTowerIndex];
            Debug.Log("Active Tower: " + activeTower.name);
        }  
        if (scrollWheelValue < 0)
        {
            activeTowerIndex = (activeTowerIndex + 1 + towers.Length) % towers.Length;
            activeTower = towers[activeTowerIndex];
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
