using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;
using UnityEngine.UI;
public class player_controller : MonoBehaviour
{
    private Rigidbody rb;

    public FixedJoystick joystick;
    public FixedJoystick bullet_joystick;
    private float moveSpeed;
    public Animator anim;
    public GameObject bullet, bulletPos;
    private bool isCreateBullet;
    public AudioSource soundSource;
    public AudioClip weaponSound;
    public AudioSource caseSource;
    public AudioClip caseSound;
    private static List<string> savedThings = new List<string>();
    private string x,y;
    public GameObject energyScript;
    private float hearth;
    private bool isValued;
    private int ammoCount;
    private bool isRun;
    public GameObject[] thingPanels;
    private bool isRunGame;
    public GameObject[] ui;
    public GameObject deadPanel;
    public TMPro.TextMeshProUGUI bulletText;
    private GameObject[] zombies;
    private float decreaseMoveSpeed;
    public GameObject[] players;
    public ParticleSystem fireLightEffect;
    //progress bar
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Image hearthImage;
    private void Start()
    {
        isRunGame = true;
        if (PlayerPrefs.GetFloat("moveSpeed") == null || PlayerPrefs.GetFloat("moveSpeed") == 0)
        {
            moveSpeed = 6f;
            PlayerPrefs.SetFloat("moveSpeed", moveSpeed);
        }
        else
        {
            moveSpeed = PlayerPrefs.GetFloat("moveSpeed");
        }
        PlayerPrefs.SetString("things", "");
        float fill = (moveSpeed / 6);
        uiFillImage.fillAmount = Mathf.Abs(fill);
        rb = GetComponent<Rigidbody>();
        decreaseMoveSpeed = PlayerPrefs.GetFloat("decreaseMoveSpeed");
        if(decreaseMoveSpeed == 0f || decreaseMoveSpeed == null)
        {
            decreaseMoveSpeed = 0.01f;
            PlayerPrefs.SetFloat("decreaseMoveSpeed", decreaseMoveSpeed);
        }
        int characterIndex = PlayerPrefs.GetInt("chooseCharacter");
        if(characterIndex == 1)
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
    private void FixedUpdate()
    {
        Application.targetFrameRate = 300;
        if (isRunGame == false)
        {
            return;
        }
            if (isValued == false)
        {
            isValued = true;
            hearth = PlayerPrefs.GetFloat("energy");
            if (hearth > 0)
            {
                float fill_2 = ((hearth) / 100);
                hearthImage.fillAmount = Mathf.Abs(fill_2);
            }
            else
            {
                float fill_2 = 0f;
                hearthImage.fillAmount = Mathf.Abs(fill_2);
            }
            ammoCount = PlayerPrefs.GetInt("ammoCount");
            bulletText.text = ammoCount.ToString();
        }
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if ((joystick.Horizontal != 0 || joystick.Vertical != 0))
        {
            anim.SetBool("isRun", true);
            transform.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0f, rb.velocity.z));
            if (moveSpeed >= 2f)
            {
                moveSpeed -= Time.deltaTime * decreaseMoveSpeed;
                PlayerPrefs.SetFloat("moveSpeed", moveSpeed);
                float fill =  ((moveSpeed) / 6);
                uiFillImage.fillAmount = Mathf.Abs(fill);
            }
        }
        else
        {
            anim.SetBool("isRun", false);
        }
        if(rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0)
        {
        }
        else
        {
        }
        if ((bullet_joystick.Horizontal != 0 || bullet_joystick.Vertical != 0) && ammoCount > 0)
        {
            anim.SetBool("isFire", true);
            transform.rotation = Quaternion.LookRotation(new Vector3(bullet_joystick.Horizontal, 0f, bullet_joystick.Vertical));
            if (isCreateBullet == false)
            {
                isCreateBullet = true;
                StartCoroutine(createBullet());
            }
        }
        else
        {
            isCreateBullet = false;
            anim.SetBool("isFire", false);
        }
    }
    IEnumerator createBullet()
    {
        yield return new WaitForSeconds(0.2f);
        fireLightEffect.Play();
        Instantiate(bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .1f);
        soundSource.PlayOneShot(weaponSound);
        isCreateBullet = false;
        ammoCount--;
        if (ammoCount < 0)
        {
            ammoCount = 0;
        }
        PlayerPrefs.SetInt("ammoCount", ammoCount);
        bulletText.text = ammoCount.ToString();

    }
    public void healthBar()
    {
        hearth = PlayerPrefs.GetFloat("energy");
        if (hearth > 0f)
        {
            float fill_2 = ((hearth) / 100);
            hearthImage.fillAmount = Mathf.Abs(fill_2);
        }
        else
        {
            zombies = GameObject.FindGameObjectsWithTag("zombie");
            foreach (GameObject go in zombies)
            {
                go.GetComponent<Animator>().SetBool("isStartAttack", false);
                go.GetComponent<Animator>().SetBool("isStartRun", false);
                go.GetComponent<Animator>().SetBool("isDead", false);
            }
            hearth = -1;
            isRunGame = false;
            anim.SetBool("isDead", true);
            PlayerPrefs.SetFloat("energy", hearth);
            PlayerPrefs.SetString("isDeadShelter", "true");
            float fill_2 = 0;
            hearthImage.fillAmount = Mathf.Abs(fill_2);
            rb.velocity = new Vector3(0f, 0f, 0f);
            foreach(GameObject go in ui)
            {
                go.SetActive(false);
            }
            StartCoroutine(showDeadPanel());
        }
    }
    IEnumerator showDeadPanel()
    {
        yield return new WaitForSeconds(4f);
        deadPanel.SetActive(true);
        Time.timeScale = 0;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "case")
        {
            caseSource.PlayOneShot(caseSound);
            collision.gameObject.GetComponent<equipment>().GetComponent<Animator>().SetBool("isOpen", true);
            x = null;
            y = null;
            savedThings.Add(collision.gameObject.GetComponent<equipment>().selected_thing);
            x = "-" + savedThings[savedThings.Count-1];
            y = PlayerPrefs.GetString("things");
            PlayerPrefs.SetString("things", y + x);
            collision.gameObject.tag = "Untagged";
            GameObject newThingPanel;
            if(savedThings[savedThings.Count - 1] == "apple")
            {
                newThingPanel = thingPanels[0];
                newThingPanel.SetActive(true);
                StartCoroutine(thingPanelClose(newThingPanel));
            }
            else if(savedThings[savedThings.Count - 1] == "soup")
            {
                newThingPanel = thingPanels[1];
                newThingPanel.SetActive(true);
                StartCoroutine(thingPanelClose(newThingPanel));
            }
            else if (savedThings[savedThings.Count - 1] == "water")
            {
                newThingPanel = thingPanels[2];
                newThingPanel.SetActive(true);
                StartCoroutine(thingPanelClose(newThingPanel));
            }
            else if (savedThings[savedThings.Count - 1] == "pill")
            {
                newThingPanel = thingPanels[3];
                newThingPanel.SetActive(true);
                StartCoroutine(thingPanelClose(newThingPanel));
            }
            else if (savedThings[savedThings.Count - 1] == "ammo")
            {
                newThingPanel = thingPanels[4];
                newThingPanel.SetActive(true);
                StartCoroutine(thingPanelClose(newThingPanel));
            }
        }
        if(collision.gameObject.tag == "shelter")
        {
            PlayerPrefs.SetString("saveThings", PlayerPrefs.GetString("saveThings") + PlayerPrefs.GetString("things"));
            PlayerPrefs.SetString("things", "");
            SceneManager.LoadScene("shelter");
        }
    }
    IEnumerator thingPanelClose(GameObject x)
    {
        yield return new WaitForSeconds(1.4f);
        x.SetActive(false);
    }
}
