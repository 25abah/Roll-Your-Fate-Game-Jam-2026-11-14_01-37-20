using UnityEngine;

public class ScrollWheelManager : MonoBehaviour
{
    public GameObject[] towers;
    GameObject activeTower;
    int activeTowerIndex = 0;
    void Start()
    {
        activeTower = towers[activeTowerIndex];
    }

    void Update()
    {
        ScrollTowers();
        SpawnTower();
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
            Instantiate(activeTower, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
