using UnityEngine;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;
public class TowerSelecter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject []frames;
    [SerializeField] private int selected = 0;
    [SerializeField] private Text bitText;

    [SerializeField] private GameObject description;
    void Start()
    {
        GameObject frameHolder = GameObject.Find("Frames");
        frames = new GameObject[frameHolder.transform.childCount];
        for(int i = 0;i<frames.Length;i++)
        {
            frames[i] = frameHolder.transform.GetChild(i).gameObject;
            frames[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        frames[selected].transform.GetChild(0).gameObject.SetActive(true);
        bitText = gameObject.transform.GetChild(0).GetComponent<Text>();
        description = gameObject.transform.GetChild(2).gameObject;


    }
    public void updateBit(int amount)
    {
        bitText.text =  "Bits :"+ amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void next()
    {
        if(selected+1 >=frames.Length)
        {
            selected = 0;
        }
        else
        {
            selected++;
        }
        change();
    }
    public void change()
    {
        for(int i = 0;i<frames.Length;i++)
        {
            frames[i].transform.GetChild(0).gameObject.SetActive(false);
        }
        frames[selected].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void previous()
    {
        if(selected-1 <0)
        {
            selected = frames.Length-1;
        }
        else
        {
            selected--;
        }
        change();
    }
    public void updateDescription(TowerBase towerbase)
    {
        Text[] descriptionTexts = new Text[description.transform.childCount-1];
        for(int i =1; i<description.transform.childCount;i++)
        {
            descriptionTexts[i-1] = description.transform.GetChild(i).GetComponent<Text>();
        }
        descriptionTexts[0].text = towerbase.name;
        descriptionTexts[1].text = "Cost : "+ towerbase.price.ToString();
        descriptionTexts[2].text = towerbase.description;
        descriptionTexts[3].text = "Range: "+ towerbase.range.ToString() + " Damage: " + towerbase.damage.ToString();

    }

}
