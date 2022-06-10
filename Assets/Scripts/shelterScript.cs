using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shelterScript : MonoBehaviour
{
    private int soupCount, waterCount, appleCount, pillCount, ammoCount;
    public TMPro.TextMeshPro[] objectTexts;
    private GameObject[] apples, waters,soups;
    private string[] a;
    private string y;
    private float energy;
    private bool isValued;
    private int currentDay, ammo_1;
    private float x,hearth;
    private float brainCount;
    public GameObject exitShelterBtn;
    private string suicideCon;
    public GameObject bookPanel;
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Image hearthImage;
    [SerializeField] private Image brainImage;
    public GameObject[] panels;
    public GameObject[] ui;
    public GameObject suicidePanel, deadPanel;
    private float moveSpeed;
    public GameObject powerUpsPanel, nextDayBtn,openBtn;
    private bool isCheckAmmo = false;
    public GameObject player;
    public TMPro.TextMeshProUGUI deadPanelText;
    public GameObject[] players;
    private GameObject openedPlayer;
    public GameObject settingsPanel;
    public AudioSource[] musics, sounds;
    public GameObject[] buttons;
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
        int haveGame = PlayerPrefs.GetInt("haveGame");
        haveGame = 1;
        PlayerPrefs.SetInt("haveGame", haveGame);
        int characterIndex = PlayerPrefs.GetInt("chooseCharacter");
        if (characterIndex == 1)
        {
            openedPlayer = players[0];
            openedPlayer.SetActive(true);
            players[1].SetActive(false);
        }
        else
        {
            players[0].SetActive(false);
            openedPlayer = players[1];
            openedPlayer.SetActive(true);
        }
        int acp = PlayerPrefs.GetInt("ammoCount");
        if(acp == 0 || acp == null)
        {
            ammoCount = 30;
            PlayerPrefs.SetInt("ammoCount", ammoCount);
        }
        else
        {
            ammoCount = acp;
        }
        if(acp < 0)
        {
            acp = 0;
            ammoCount = 30;
            PlayerPrefs.SetInt("ammoCount", ammoCount);
        }
        string suiTxt = PlayerPrefs.GetString("isSuicide");
        if(suiTxt == "" || suiTxt == null)
        {
            PlayerPrefs.SetString("isSuicide", "false");
        }
        else
        {
            suicideCon = PlayerPrefs.GetString("isSuicide");
        }
        string deadText = PlayerPrefs.GetString("isDeadShelter");
        if (deadText == "" || deadText == null)
        {
            PlayerPrefs.SetString("isDeadShelter", "false");
        }
        else
        {
            deadText = PlayerPrefs.GetString("isDeadShelter");
        }
        float bcp = PlayerPrefs.GetFloat("brainCount");
        if (deadText == "true")
        {
            foreach (GameObject go in ui)
            {
                go.SetActive(false);
            }
            deadPanel.SetActive(true);
            deadPanelText.text = "You starved to death in this cold shelter on the " + PlayerPrefs.GetInt("currentDay").ToString() + " day";
            openedPlayer.SetActive(false);
        }
        if (suicideCon == "true")
        {
            bcp = 0f;
            foreach(GameObject go in ui)
            {
                go.SetActive(false);
            }
            suicidePanel.SetActive(true);
            openedPlayer.SetActive(false);
        }
        else
        {
            if (bcp == 0f || bcp == null || brainCount > 100f)
            {
                brainCount = 100;
                PlayerPrefs.SetFloat("brainCount", brainCount);
            }
            else
            {
                brainCount = PlayerPrefs.GetFloat("brainCount");
            }
        }
        
        float fill_3 = ((brainCount) / 100f);
        brainImage.fillAmount = Mathf.Abs(fill_3);
        ammo_1 = 0;
        if (PlayerPrefs.GetString("saveThings") != null || PlayerPrefs.GetString("saveThings") != "")
        {
            y = null;
            a = PlayerPrefs.GetString("saveThings").Split(char.Parse("-"));
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] == "soup")
                {
                    soupCount++;
                }
                else if (a[i] == "water")
                {
                    waterCount++;
                }
                else if (a[i] == "apple")
                {
                    appleCount++;
                }
                else if (a[i] == "pill")
                {
                    pillCount++;
                }
                else if(a[i] == "ammo")
                {
                    ammo_1++;
                    a[i] = " ";
                    isCheckAmmo = true;
                }
            }
            if(isCheckAmmo == true)
            {
                isCheckAmmo = false;
                ammo_1 *= 30;
                ammoCount += ammo_1;
                PlayerPrefs.SetInt("ammoCount", ammoCount);
            }
            PlayerPrefs.SetInt("pillCount", pillCount);
            string newVal = "";
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] != " " && a[i] != "")
                {
                    newVal = newVal + "-" + a[i];
                }
            }
            PlayerPrefs.SetString("saveThings", newVal);
        }
        int aa = PlayerPrefs.GetInt("currentDay");
        if (aa == null || aa == 0)
        {
            currentDay = 1;
            PlayerPrefs.SetInt("currentDay", currentDay);
        }
        else
        {
            currentDay = PlayerPrefs.GetInt("currentDay");
        }
        objectTexts[0].text = soupCount.ToString();
        objectTexts[1].text = appleCount.ToString();
        objectTexts[2].text = waterCount.ToString();
        objectTexts[3].text = pillCount.ToString();
        objectTexts[4].text = ammoCount.ToString();
        if (PlayerPrefs.GetString("exitShelter") == "true")
        {
            exitShelterBtn.SetActive(false);
        }
        newText();
        x = PlayerPrefs.GetFloat("moveSpeed");
        if (x == null || x == 0)
        {
            moveSpeed = 6f;
            PlayerPrefs.SetFloat("moveSpeed", moveSpeed);
        }
        else
        {
            moveSpeed = x;
        }
        float fill = ((moveSpeed) / 6f);
        uiFillImage.fillAmount = Mathf.Abs(fill);
        hearth = PlayerPrefs.GetFloat("energy");
        if(hearth == null || hearth == 0)
        {
            hearth = 100f;
            PlayerPrefs.SetFloat("energy", hearth);
        }
        else
        {
            hearth = PlayerPrefs.GetFloat("energy");
        }
        float fill_2 = ((hearth) / 100);
        hearthImage.fillAmount = Mathf.Abs(fill_2);
    }
    public void Update()
    {
        if (isValued == false)
        {
            isValued = true;
            energy = PlayerPrefs.GetFloat("energy");
        }
        if (PlayerPrefs.GetString("exitShelter") == "true")
        {
            exitShelterBtn.SetActive(false);
        }
        string music_con = PlayerPrefs.GetString("music");
        if (music_con == "open")
        {
            foreach (AudioSource a in musics)
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
    }
    public void nextDay()
    {
        energy = PlayerPrefs.GetFloat("energy");
        energy -= 8f;
        if (energy>0f)
        {
            int skillPoint = PlayerPrefs.GetInt("skillPoint");
            if (skillPoint == 0 || skillPoint == null)
            {
                skillPoint = 0;
                PlayerPrefs.SetInt("skillPoint", skillPoint);
            }
            skillPoint++;
            PlayerPrefs.SetInt("skillPoint", skillPoint);
            PlayerPrefs.SetFloat("energy", energy);
            energy = PlayerPrefs.GetFloat("energy");
            currentDay++;
            PlayerPrefs.SetInt("currentDay", currentDay);
            isValued = false;
            PlayerPrefs.SetString("exitShelter", "false");
            PlayerPrefs.SetFloat("moveSpeed", PlayerPrefs.GetFloat("moveSpeed") + 0.55f);
            float aa = PlayerPrefs.GetFloat("moveSpeed");
            if (aa > 6f)
            {
                PlayerPrefs.SetFloat("moveSpeed", 6f);
            }
            float x = PlayerPrefs.GetFloat("moveSpeed");
            if (x > 6f)
            {
                PlayerPrefs.SetFloat("moveSpeed",6f);
                x = PlayerPrefs.GetFloat("moveSpeed");
            }
            brainCount = PlayerPrefs.GetFloat("brainCount");
            if (brainCount >= 10f)
            {
                brainCount -= 10f;
                PlayerPrefs.SetFloat("brainCount", brainCount);
                if (brainCount <= 0f)
                {
                    PlayerPrefs.SetString("isSuicide", "true");
                }
                else
                {
                    PlayerPrefs.SetString("isSuicide", "false");
                }
            }
            SceneManager.LoadScene("shelter");
        }
        else
        {
            PlayerPrefs.SetString("isDeadShelter", "true");
            SceneManager.LoadScene("shelter");
        }
    }
    public void exitShelter()
    {
        PlayerPrefs.SetString("exitShelter", "true");
        brainCount = PlayerPrefs.GetFloat("brainCount");
        brainCount += 10;
        PlayerPrefs.SetFloat("brainCount", brainCount);
        SceneManager.LoadScene("SampleScene");
    }
    public void newText()
    {
        apples = GameObject.FindGameObjectsWithTag("apple");
        int apCo = 0;
        foreach (GameObject c in apples)
        {
            apCo++;
        }
        if (apCo > appleCount)
        {
            for (int i = 0; i < Mathf.Abs(appleCount - apCo); i++)
            {
                Destroy(apples[i]);
            }
        }
        waters = GameObject.FindGameObjectsWithTag("water");
        int waCo = 0;
        foreach (GameObject c in waters)
        {
            waCo++;
        }
        if (waCo > waterCount)
        {
            for (int i = 0; i < Mathf.Abs(waterCount - waCo); i++)
            {
                Destroy(waters[i]);
            }
        }
        soups = GameObject.FindGameObjectsWithTag("soup");
        int soCo = 0;
        foreach (GameObject c in soups)
        {
            soCo++;
        }
        if (soCo > soupCount)
        {
            for (int i = 0; i < Mathf.Abs(soupCount - soCo); i++)
            {
                Destroy(soups[i]);
            }
        }
    }
    public void useThing(string useThings)
    {
        int count;
        bool isIncrease = false;
        if (useThings == "soup")
        {
            count = soupCount;
            if (count > 0)
            {
                isIncrease = true;
                hearth += 12f;
            }
        }
        else if (useThings == "water")
        {
            count = waterCount;
            if(count > 0)
            {
                isIncrease = true;
                hearth += 6f;
            }
        }
        else if (useThings == "apple")
        {
            count = appleCount;
            if (count > 0)
            {
                isIncrease = true;
                hearth += 9f;
            }
        }
        else if (useThings == "pill")
        {
            count = pillCount;
            if( count > 0)
            {
                isIncrease = true;
                brainCount += 30f;
            }
        }
        if (hearth > 100f)
        {
            hearth = 100f;
        }
        if(brainCount > 100f)
        {
            brainCount = 100f;
        }
        if(isIncrease == true)
        {
            isIncrease = false;
            float fill_3 = ((brainCount) / 100f);
            brainImage.fillAmount = Mathf.Abs(fill_3);
            PlayerPrefs.SetFloat("brainCount", brainCount);
            float fill_2 = ((hearth) / 100);
            hearthImage.fillAmount = Mathf.Abs(fill_2);
            PlayerPrefs.SetFloat("energy", hearth);
            string newVal = "";
            if (PlayerPrefs.GetString("saveThings") != null || PlayerPrefs.GetString("saveThings") != "")
            {
                y = null;
                a = PlayerPrefs.GetString("saveThings").Split(char.Parse("-"));
                for (int i = 1; i < a.Length; i++)
                {
                    if (a[i] == useThings)
                    {
                        a[i] = " ";
                        break;
                    }
                }
                for (int i = 1; i < a.Length; i++)
                {
                    if (a[i] != " " && a[i] != "")
                    {
                        newVal = newVal + "-" + a[i];
                    }
                }
            }
            PlayerPrefs.SetString("saveThings", newVal);
            soupCount = 0;
            waterCount = 0;
            appleCount = 0;
            pillCount = 0;
            if (PlayerPrefs.GetString("saveThings") != null || PlayerPrefs.GetString("saveThings") != "")
            {
                y = null;
                a = PlayerPrefs.GetString("saveThings").Split(char.Parse("-"));
                for (int i = 1; i < a.Length; i++)
                {
                    if (a[i] == "soup")
                    {
                        soupCount++;
                    }
                    else if (a[i] == "water")
                    {
                        waterCount++;
                    }
                    else if (a[i] == "apple")
                    {
                        appleCount++;
                    }
                    else if (a[i] == "pill")
                    {
                        pillCount++;
                    }
                    else if (a[i] == "ammo")
                    {
                        ammo_1++;
                        a[i] = " ";
                        isCheckAmmo = true;
                    }
                }
                if (isCheckAmmo == true)
                {
                    isCheckAmmo = false;
                    ammo_1 *= 30;
                    ammoCount += ammo_1;
                    PlayerPrefs.SetInt("ammoCount", ammoCount);
                }
            }
            newText();
            objectTexts[0].text = soupCount.ToString();
            objectTexts[1].text = appleCount.ToString();
            objectTexts[2].text = waterCount.ToString();
            objectTexts[3].text = pillCount.ToString();
            objectTexts[4].text = ammoCount.ToString();
        }
    }
    public void powerUpsOpen()
    {
        powerUpsPanel.SetActive(true);
        nextDayBtn.SetActive(false);
        exitShelterBtn.SetActive(false);
        openBtn.SetActive(false);
    }
    public void powerUpsClose()
    {
        powerUpsPanel.SetActive(false);
        nextDayBtn.SetActive(true);
        if (PlayerPrefs.GetString("exitShelter") == "false")
        {
            exitShelterBtn.SetActive(true);
        }
        openBtn.SetActive(true);
    }
    public void openSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void closeSettings()
    {
        settingsPanel.SetActive(false);
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
