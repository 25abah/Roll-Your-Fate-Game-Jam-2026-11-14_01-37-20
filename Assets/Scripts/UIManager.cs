using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Text timerText;
    [SerializeField] private Slider hpBar;
    [SerializeField] private TowerSelecter towerSelecter;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject gameOverScreen;

    void Start()
    {
        gameOverScreen = gameObject.transform.GetChild(4).gameObject;
        titleScreen = gameObject.transform.GetChild(3).gameObject;
        timerText = gameObject.transform.GetChild(2).GetComponent<Text>();
        hpBar = gameObject.transform.GetChild(1).GetComponent<Slider>();
        towerSelecter = gameObject.transform.GetChild(0).GetComponent<TowerSelecter>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateTimer( float time)
    {
        timerText.text = time.ToString() + " S";
        
    }
    public void updatehpBar( float hp)
    {
        hpBar.value = hp;
    }
    public void nextTower()
    {
        towerSelecter.next();

    }
    public void previousTower()
    {
        towerSelecter.previous();
    }
    public void disableTitleScreen()
    {
        titleScreen.SetActive(false);
    }
    public void enableTitleScreen()
    {
        titleScreen.SetActive(true);
    }
    public void disableGameOver()
    {
        gameOverScreen.SetActive(false);
    }
    public void enableGameOver()
    {
        gameOverScreen.SetActive(false);
    }
    public void updateDescription(string name,int cost, string unitDescription,string stats)
    {
        towerSelecter.updateDescription(name,cost,unitDescription,stats);
    }



}
