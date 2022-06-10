using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EZCameraShake;
using UnityEngine.UI;
public class player_controller_tutorial : MonoBehaviour
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
    private static List<string> savedThings = new List<string>();
    private int ammoCount;
    public GameObject[] thingPanels;
    private bool isRunGame;
    public TMPro.TextMeshProUGUI bulletText;
    private GameObject[] zombies;
    private float decreaseMoveSpeed = 0.05f;
    [SerializeField] private Image uiFillImage;
    [SerializeField] private Image hearthImage;
    private void Start()
    {
        isRunGame = true;
        moveSpeed = 6f;
        rb = GetComponent<Rigidbody>();
        ammoCount = 50;
        //bulletText.text = ammoCount.ToString();
        PlayerPrefs.SetInt("isCaseTouch", 0);
    }
    private void FixedUpdate()
    {
        Application.targetFrameRate = 300;
        if (isRunGame == false)
        {
            return;
        }
        rb.velocity = new Vector3(joystick.Horizontal * moveSpeed, rb.velocity.y, joystick.Vertical * moveSpeed);
        if ((joystick.Horizontal != 0 || joystick.Vertical != 0))
        {
            transform.rotation = Quaternion.LookRotation(rb.velocity);
            if (moveSpeed >= 2f)
            {
                moveSpeed -= Time.deltaTime * decreaseMoveSpeed;
                float fill = ((moveSpeed) / 6);
                uiFillImage.fillAmount = Mathf.Abs(fill);
            }
        }
        else
        {

        }
        if (rb.velocity.x == 0 && rb.velocity.y == 0 && rb.velocity.z == 0)
        {
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
        }
        if ((bullet_joystick.Horizontal != 0 || bullet_joystick.Vertical != 0) && ammoCount > 0)
        {
            anim.SetBool("isFire", true);
            transform.rotation = Quaternion.LookRotation(new Vector3(bullet_joystick.Horizontal, rb.velocity.y, bullet_joystick.Vertical));
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
        Instantiate(bullet, bulletPos.transform.position, bulletPos.transform.rotation);
        CameraShaker.Instance.ShakeOnce(2f, 2f, .1f, .1f);
        soundSource.PlayOneShot(weaponSound);
        isCreateBullet = false;
        //bulletText.text = ammoCount.ToString();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "case")
        {
            PlayerPrefs.SetInt("isCaseTouch", 1);
            collision.gameObject.GetComponent<equipment>().GetComponent<Animator>().SetBool("isOpen", true);
            savedThings.Add(collision.gameObject.GetComponent<equipment>().selected_thing);
            GameObject newThingPanel;
            if (savedThings[savedThings.Count - 1] == "apple")
            {
                newThingPanel = thingPanels[0];
                newThingPanel.SetActive(true);
                StartCoroutine(thingPanelClose(newThingPanel));
            }
            else if (savedThings[savedThings.Count - 1] == "soup")
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
        IEnumerator thingPanelClose(GameObject x)
        {
            yield return new WaitForSeconds(1.4f);
            x.SetActive(false);
        }
    }
}
