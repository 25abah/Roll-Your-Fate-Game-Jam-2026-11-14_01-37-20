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
    [SerializeField] private Text roundText;


    void Start()
    {
        roundText = gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        gameOverScreen = gameObject.transform.GetChild(5).gameObject;
        titleScreen = gameObject.transform.GetChild(4).gameObject;
        timerText = gameObject.transform.GetChild(2).GetComponent<Text>();
        hpBar = gameObject.transform.GetChild(1).GetComponent<Slider>();
        towerSelecter = gameObject.transform.GetChild(0).GetComponent<TowerSelecter>();

    }

    public void updateCurrency(int currency)
    {
        towerSelecter.updateBit(currency);
    }
    public void updateTimer( float time)
    {
        timerText.text = time.ToString("0.00") + "s";
        
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
        gameOverScreen.SetActive(true);
    }
    public void updateDescription(TowerBase towerbase)
    {
        towerSelecter.updateDescription(towerbase);
    }
    public void updateRoundText(int roundNumber,GameState gameState)
    {
        roundText.text = "Round "+roundNumber.ToString() + "("+gameState.ToString()+")";
    }
    



}
