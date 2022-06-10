using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start_manager : MonoBehaviour
{
    public Button contBtn;
    public GameObject settingsPanel;
    public AudioSource[] musics,sounds;
    public GameObject[] buttons;
    public GameObject bg;
    public GameObject chooseCharacter, chooseCharacterCam;
    public GameObject[] players;
    public Animator camAnim;
    void Start()
    {
        int characterIndex = PlayerPrefs.GetInt("chooseCharacter");
        if (characterIndex == 1)
        {
            players[0].SetActive(true);
            players[1].SetActive(false);
        }
        else
        {
            players[0].SetActive(false);
            players[1].SetActive(true);
        }
        string music_con = PlayerPrefs.GetString("music");
        if (music_con == "" || music_con == null || music_con == " " || music_con == "open")
        {
            music_con = "open";
            PlayerPrefs.SetString("music", music_con);
            buttons[0].SetActive(false);
            buttons[1].SetActive(true);
        }
        else
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
        }
        string sound_con = PlayerPrefs.GetString("sound");
        if (sound_con == "" || sound_con == null || sound_con == " " || sound_con == "open")
        {
            sound_con = "open";
            PlayerPrefs.SetString("sound", sound_con);
            buttons[2].SetActive(false);
            buttons[3].SetActive(true);
        }
        else
        {
            buttons[2].SetActive(true);
            buttons[3].SetActive(false);
        }
        Application.targetFrameRate = 300;
        int haveGame = PlayerPrefs.GetInt("haveGame");
        string deadControl = PlayerPrefs.GetString("isDeadShelter");
        string suicideControl = PlayerPrefs.GetString("isSuicide");
        if (haveGame == 0 || deadControl == "true" || suicideControl == "true")
        {
            contBtn.interactable = false;
        }
        else
        {
            contBtn.interactable = true;
        }
    }
    private void Update()
    {
        string music_con = PlayerPrefs.GetString("music");
        if (music_con == "open")
        {
            foreach(AudioSource a in musics)
            {
                a.mute = true;
            }
        }
        else
        {
            foreach (AudioSource a in musics)
            {
                a.mute = false;
            }
        }
        string sound_con = PlayerPrefs.GetString("sound");
        if (sound_con == "open")
        {
            foreach (AudioSource a in sounds)
            {
                a.mute = true;
            }
        }
        else
        {
            foreach (AudioSource a in sounds)
            {
                a.mute = false;
            }
        }
        int characterIndex = PlayerPrefs.GetInt("chooseCharacter");
        if (characterIndex == 1)
        {
            players[0].SetActive(true);
            players[1].SetActive(false);
        }
        else
        {
            players[0].SetActive(false);
            players[1].SetActive(true);
        }
    }
    public void contGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("shelter");
    }
    public void openChoosePanel()
    {
        bg.SetActive(false);
        chooseCharacterCam.SetActive(true);
        chooseCharacter.SetActive(true);
    }
    public void closeChoosePanel()
    {
        bg.SetActive(true);
        chooseCharacterCam.SetActive(false);
        chooseCharacter.SetActive(false);
    }
    public void newGame(int playerIndex)
    {
        PlayerPrefs.SetInt("chooseCharacter", playerIndex);
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
        PlayerPrefs.SetFloat("decreaseMoveSpeed", 0);
        PlayerPrefs.SetFloat("weaponDamage", 0);
        PlayerPrefs.SetFloat("shieldPower", 0);
        PlayerPrefs.SetString("isSuicide", "false");
        PlayerPrefs.SetString("isDeadShelter", "false");
        SceneManager.LoadScene("shelter");
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void music()
    {
        string music_con = PlayerPrefs.GetString("music");
        if (music_con == "open")
        {
            buttons[0].SetActive(true);
            buttons[1].SetActive(false);
            music_con = "close";
            PlayerPrefs.SetString("music", music_con);
        }
        else
        {
            buttons[0].SetActive(false);
            buttons[1].SetActive(true);
            music_con = "open";
            PlayerPrefs.SetString("music", music_con);
        }
    }
    public void sound()
    {
        string sound_con = PlayerPrefs.GetString("sound");
        if (sound_con == "open")
        {
            buttons[2].SetActive(true);
            buttons[3].SetActive(false);
            sound_con = "close";
            PlayerPrefs.SetString("sound", sound_con);
        }
        else
        {
            buttons[2].SetActive(false);
            buttons[3].SetActive(true);
            sound_con = "open";
            PlayerPrefs.SetString("sound", sound_con);
        }
    }
    public void tutorial()
    {
        SceneManager.LoadScene("tutorial_shelter");
    }
    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void closeSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void leftPlayer()
    {
        camAnim.SetBool("isLeft", true);
    }
    public void rightPlayer()
    {
        camAnim.SetBool("isRight", true);
    }
    public void normalCam()
    {
        camAnim.SetBool("isLeft", false);
        camAnim.SetBool("isRight", false);
    }
}
