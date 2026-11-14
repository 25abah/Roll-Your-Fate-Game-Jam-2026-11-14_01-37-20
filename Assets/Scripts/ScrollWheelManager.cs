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
    GameObject activeTower;
    int activeTowerIndex = 0;
    public GameState currentGameState = GameState.SetupPhase;
    Gameplay gameplay;
    void Start()
    {
        activeTower = towers[activeTowerIndex];
        gameplay = GetComponent<Gameplay>();
    }

    void Update()
    {
        if (currentGameState == GameState.SetupPhase || currentGameState == GameState.WavePhase)
        {
            ScrollTowers();
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

    void SpawnTower(){
        if (Input.GetMouseButtonDown(2))
        {
            Instantiate(activeTower, FindMousePosition(), Quaternion.identity);
            activeTower.transform.position = new Vector3(activeTower.transform.position.x, activeTower.transform.position.y, 0);
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
