using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial_manager : MonoBehaviour
{
    private int indexPage = -2;
    private string[] texts = { "The places you see in the back are where your supplies are. From left to right: These are where the medicines, bullets, apples, water, and soup are, and the place above them shows how many of them are.",
                            "Clicking on this place will make you fall asleep, but your health and sanity will decrease but your running energy will increase.",
                            "Clicking on this place will open a panel where you can use your materials. In the panel that opens, if you have it, you can use it when you click it.",
                            "Clicking on this place will take you out of the house.",
                            "This place will give you more detailed information about the game.",
                            "This place will open a panel for you to develop your character. In the panel that opens, the '?' you can access more details by clicking the button.",
                            "This place is the bar that shows your running energy.",
                            "This place is the bar that shows your health.",
                            "This place is the bar that shows your sanity."};
    public GameObject[] hands;
    public TMPro.TextMeshProUGUI tutorialText;
    private GameObject openedHand;
    public GameObject tutorialBG, tutorialPanel;
    private int outIndexPage = -1;
    private string[] outdoorTexts = {"This joystick lets you move.",
                                     "This joystick lets you fire.",
                                     "This is a minimap. It allows you to see the map more easily.",
                                     "This is where your bullets are.",
                                     "Hey, there's an enemy ahead, see? Come on kill him!",
                                     "I think the town is in danger. Anyway, you can get new items by collecting crates here. Go to them and they will automatically give you items.",
                                     "Yes, that's all, now you're ready!"};
    public GameObject zombie,thingCase, lastPanel;
    public bool isOpenTutorial, isOpenTutorial2;
    public GameObject[] ui;
    public void nextTutorial()
    {
        if (indexPage >= 7)
        {
            openedHand.SetActive(false);
            tutorialText.text = "Oh, what are those sounds? There is only one gun, you take it. Let's go outside and find out what these sounds are.";
            hands[2].SetActive(true);
            Destroy(tutorialBG);
            return;
        }
        indexPage++;
        Debug.Log(indexPage);
        if(indexPage == -1)
        {
            tutorialText.text = texts[0];
        }
        else if(indexPage == 0)
        {
            openedHand = hands[indexPage];
            openedHand.SetActive(true);
            tutorialText.text = texts[indexPage+1];
        }
        else if(indexPage > 0 && indexPage<8)
        {
            openedHand.SetActive(false);
            openedHand = hands[indexPage];
            openedHand.SetActive(true);
            tutorialText.text = texts[indexPage+1];
        }
    }
    public void exitShelterTutorial()
    {
        SceneManager.LoadScene("tutorial_game");
    }
    public void outDoorNextTutorial()
    {
        outIndexPage++;
        if (outIndexPage < 4 && outIndexPage>=0)
        {
            if(openedHand != null)
            {
                openedHand.SetActive(false);
            }
            openedHand = hands[outIndexPage];
            openedHand.SetActive(true);
            tutorialText.text = outdoorTexts[outIndexPage];
        }
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            return;
        }
        if (outIndexPage == 4)
        {
            outIndexPage++;
            tutorialBG.SetActive(false);
            StartCoroutine(tutPanelClose());
            openedHand.SetActive(false);
            tutorialText.text = outdoorTexts[outIndexPage-1];
        }
        if (zombie.GetComponent<tutZombie>().health <= 0f && isOpenTutorial == false && outIndexPage == 5)
        {
            isOpenTutorial = true;
            tutorialPanel.SetActive(true);
            tutorialText.text = outdoorTexts[outIndexPage];
            StartCoroutine(tutPanelClose2());
            outIndexPage++;
        }
        int x = PlayerPrefs.GetInt("isCaseTouch");
        if(x == 1 && isOpenTutorial2 == false)
        {
            isOpenTutorial2 = true;
            lastPanelShow();
        }
    }
    public void lastPanelShow()
    {
        tutorialPanel.SetActive(true);
        outIndexPage++;
        tutorialText.text = outdoorTexts[outIndexPage - 1];
        StartCoroutine(tutPanelClose3());
    }
    IEnumerator tutPanelClose()
    {
        yield return new WaitForSeconds(3f);
        tutorialPanel.SetActive(false);
    }
    IEnumerator tutPanelClose2()
    {
        yield return new WaitForSeconds(8f);
        tutorialPanel.SetActive(false);
        thingCase.SetActive(true);
    }
    IEnumerator tutPanelClose3()
    {
        yield return new WaitForSeconds(7f);
        tutorialPanel.SetActive(false);
        foreach(GameObject go in ui)
        {
            go.SetActive(false);
        }
        lastPanel.SetActive(true);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("start");
    }
    public void exitTutorial()
    {
        SceneManager.LoadScene("start");
    }
}
