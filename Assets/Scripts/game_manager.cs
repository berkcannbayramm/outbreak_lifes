using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{
    public GameObject[] ui;
    public GameObject pauseBG, settingsPanel;
    public void pauseGame()
    {
        pauseBG.SetActive(true);
        foreach(GameObject go in ui)
        {
            go.SetActive(false);
        }
        Time.timeScale = 0;
    }
    public void resetGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetFloat("energy", 0);
        PlayerPrefs.SetString("saveThings", "");
        PlayerPrefs.SetInt("ammoCount", 0);
        PlayerPrefs.SetFloat("brainCount", 0);
        PlayerPrefs.SetString("isSuicide", "false");
        PlayerPrefs.SetInt("currentDay", 0);
        PlayerPrefs.SetString("exitShelter", "false");
        PlayerPrefs.SetFloat("moveSpeed", 6f);
        PlayerPrefs.SetInt("runCount", 0);
        PlayerPrefs.SetInt("weaponCount", 0);
        PlayerPrefs.SetInt("shieldCount", 0);
        PlayerPrefs.SetInt("skillPoint", 0);
        PlayerPrefs.SetFloat("decreaseMoveSpeed", 0f);
        PlayerPrefs.SetFloat("weaponPower", 0);
        PlayerPrefs.SetFloat("shieldPower", 0);
        PlayerPrefs.SetString("isSuicide", "false");
        PlayerPrefs.SetString("isDeadShelter", "false");
        SceneManager.LoadScene("shelter");
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseBG.SetActive(false);
        foreach (GameObject go in ui)
        {
            go.SetActive(true);
        }
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("start");
    }
    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void closeSettings()
    {
        settingsPanel.SetActive(false);
    }
}
