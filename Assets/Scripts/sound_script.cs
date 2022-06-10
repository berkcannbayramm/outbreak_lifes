using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_script : MonoBehaviour
{
    public AudioSource[] musics, sounds;
    public GameObject[] buttons;
    private bool isBossFound;
    private GameObject boss;
    void Start()
    {
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
    }
    void Update()
    {
        if(isBossFound == false)
        {
            if (GameObject.Find("boss_music")){
                isBossFound = true;
                musics[1] = GameObject.Find("boss_music").GetComponent<AudioSource>();
            }
        }
        
        string music_con = PlayerPrefs.GetString("music");
        if (music_con == "open")
        {
            if(isBossFound == true)
            {
                foreach (AudioSource a in musics)
                {
                    a.mute = true;
                }
            }
            else
            {
                musics[0].mute = true;
            }
        }
        else
        {
            if (isBossFound == true)
            {
                foreach (AudioSource a in musics)
                {
                    a.mute = false;
                }
            }
            else
            {
                musics[0].mute = false;
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
}
