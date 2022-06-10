using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shop_manager : MonoBehaviour
{
    public GameObject[] ui;
    public GameObject shopBG;
    public TMPro.TextMeshProUGUI[] upgradeTexts;
    public Button[] upgradeButtons;
    public TMPro.TextMeshProUGUI[] upgradeButtonsText;
    private int runCount, weaponCount, shieldCount;
    public TMPro.TextMeshProUGUI skillPointText;
    private float decreaseMoveSpeed, weaponDamage, shieldPowerDamage;
    public GameObject hintPanel;
    public TMPro.TextMeshProUGUI hintContentText;
    private int skillPoint;
    void Start()
    {
        runCount = PlayerPrefs.GetInt("runCount");
        if(runCount == 0 || runCount == null)
        {
            runCount = 1;
            PlayerPrefs.SetInt("runCount", runCount);
        }
        if (runCount == 15)
        {
            upgradeButtons[0].interactable = false;
            upgradeButtonsText[0].text = "Full";
        }
        weaponCount = PlayerPrefs.GetInt("weaponCount");
        if (weaponCount == 0 || weaponCount == null)
        {
            weaponCount = 1;
            PlayerPrefs.SetInt("weaponCount", weaponCount);
        }
        if (weaponCount == 15)
        {
            upgradeButtons[1].interactable = false;
            upgradeButtonsText[1].text = "Full";
        }
        shieldCount = PlayerPrefs.GetInt("shieldCount");
        if (shieldCount == 0 || shieldCount == null)
        {
            shieldCount = 1;
            PlayerPrefs.SetInt("shieldCount", shieldCount);
        }
        if (shieldCount == 15)
        {
            upgradeButtons[2].interactable = false;
            upgradeButtonsText[2].text = "Full";
        }
        upgradeTexts[0].text = runCount.ToString();
        upgradeTexts[1].text = weaponCount.ToString();
        upgradeTexts[2].text = shieldCount.ToString();
        decreaseMoveSpeed = PlayerPrefs.GetFloat("decreaseMoveSpeed");
        if (decreaseMoveSpeed == 0f || decreaseMoveSpeed == null)
        {
            decreaseMoveSpeed = 0.01f;
            PlayerPrefs.SetFloat("decreaseMoveSpeed", decreaseMoveSpeed);
        }
        weaponDamage = PlayerPrefs.GetFloat("weaponPower");
        if (weaponDamage == 0f || weaponDamage == null)
        {
            weaponDamage = 15f;
            PlayerPrefs.SetFloat("weaponDamage", weaponDamage);
        }
        shieldPowerDamage = PlayerPrefs.GetFloat("shieldPower");
        if (shieldPowerDamage == 0f || shieldPowerDamage == null)
        {
            shieldPowerDamage = 3f;
            PlayerPrefs.SetFloat("shieldPower", shieldPowerDamage);
        }
        skillPoint = PlayerPrefs.GetInt("skillPoint");
        if (skillPoint == 0 || skillPoint == null)
        {
            skillPoint = 0;
            PlayerPrefs.SetInt("skillPoint", skillPoint);
        }
    }
    public void openShop()
    {
        foreach(GameObject go in ui)
        {
            go.SetActive(false);
        }
        shopBG.SetActive(true);
        int skillPoint = PlayerPrefs.GetInt("skillPoint");
        skillPointText.text = skillPoint.ToString();
    }
    public void closeShop()
    {
        foreach (GameObject go in ui)
        {
            go.SetActive(true);
        }
        shopBG.SetActive(false);
    }
    public void runBtn()
    {
        if (runCount < 10 && skillPoint>=3)
        {
            skillPoint -= 3;
            PlayerPrefs.SetInt("skillPoint", skillPoint);
            skillPointText.text = skillPoint.ToString();
            float decrease = 0.01f - (0.00075f * runCount);
            runCount++;
            PlayerPrefs.SetInt("runCount", runCount);
            PlayerPrefs.SetFloat("decreaseMoveSpeed", decrease);
            upgradeTexts[0].text = runCount.ToString();
            if(runCount == 10)
            {
                upgradeButtons[0].interactable = false;
                upgradeButtonsText[0].text = "Full";
            }
        }
    }
    public void weaponBtn()
    {
        if (weaponCount < 10 && skillPoint >=2)
        {
            skillPoint -= 2;
            PlayerPrefs.SetInt("skillPoint", skillPoint);
            skillPointText.text = skillPoint.ToString();
            float weaponPower = 15f + (2f * weaponCount);
            weaponCount++;
            PlayerPrefs.SetInt("weaponCount", weaponCount);
            PlayerPrefs.SetFloat("weaponPower", weaponPower);
            upgradeTexts[1].text = weaponCount.ToString();
            if (weaponCount == 10)
            {
                upgradeButtons[1].interactable = false;
                upgradeButtonsText[1].text = "Full";
            }
        }
    }
    public void shieldBtn()
    {
        if (shieldCount < 10 && skillPoint>=2)
        {
            skillPoint -= 2;
            PlayerPrefs.SetInt("skillPoint", skillPoint);
            skillPointText.text = skillPoint.ToString();
            float shieldPower = 3f - (0.015f * shieldCount);
            shieldCount++;
            PlayerPrefs.SetInt("shieldCount", shieldCount);
            PlayerPrefs.SetFloat("shieldPower", shieldPower);
            upgradeTexts[2].text = shieldCount.ToString();
            if (shieldCount == 10)
            {
                upgradeButtons[2].interactable = false;
                upgradeButtonsText[2].text = "Full";
            }
        }
    }
    public void hintPanelOpen(string powerUpName)
    {
        hintPanel.SetActive(true);
        if(powerUpName == "run")
        {
            hintContentText.text = "As we walk outside, our walking energy decreases. This buff slows this energy reduction rate.";
        }
        else if(powerUpName == "weapon")
        {
            hintContentText.text = "Increases the damage we deal to enemies.";
        }
        else if(powerUpName == "shield")
        {
            hintContentText.text = "Reduces damage taken from enemies.";
        }
    }
    public void hintPanelClose()
    {
        hintPanel.SetActive(false);
    }
}
