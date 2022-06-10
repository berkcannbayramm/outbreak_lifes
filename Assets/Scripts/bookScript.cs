using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bookScript : MonoBehaviour
{
    public GameObject[] contents;
    public GameObject[] panels;
    public GameObject bookPanel;
    private float moveSpeed, healthCount, brainCount;
    private int dayCount;
    private string textValue;
    public TMPro.TextMeshProUGUI diaryContent, diaryDayText;
    public void openBook()
    {
        healthCount = PlayerPrefs.GetFloat("energy");
        moveSpeed = PlayerPrefs.GetFloat("moveSpeed");
        brainCount = PlayerPrefs.GetFloat("brainCount");
        dayCount = PlayerPrefs.GetInt("currentDay");

        diaryDayText.text = dayCount.ToString();
        textValue = "";
        if (healthCount >= 70)
        {
            textValue += "You are health high.";
        }
        else if (healthCount <= 40)
        {
            textValue += "You are health low.";
        }
        else
        {
            textValue += "You are health medium.";
        }
        textValue += "\n\n";
        if (brainCount >= 70)
        {
            textValue += "You are sanity high.";
        }
        else if (brainCount <= 40)
        {
            textValue += "You are sanity low.";
        }
        else
        {
            textValue += "You are sanity medium.";
        }
        textValue += "\n\n";
        if (moveSpeed >= 4f)
        {
            textValue += "You are not tired.";
        }
        else
        {
            textValue += "You are tired.";
        }
        diaryContent.text = textValue;
        bookPanel.SetActive(true);
        foreach (GameObject c in panels)
        {
            c.SetActive(false);
        }
    }
    public void closeBook()
    {
        bookPanel.SetActive(false);
        foreach (GameObject c in panels)
        {
            c.SetActive(true);
        }
    }
    public void openContent(string category)
    {
        foreach (GameObject go in contents)
        {
            go.SetActive(false);
        }
        if(category == "diary")
        {
            contents[0].SetActive(true);
        }
        else if(category == "equipment")
        {
            contents[1].SetActive(true);
        }
        else if (category == "bars")
        {
            contents[2].SetActive(true);
        }
        else if (category == "buttons")
        {
            contents[3].SetActive(true);
        }
    }
}