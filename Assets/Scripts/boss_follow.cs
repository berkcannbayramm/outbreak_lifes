using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss_follow : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Target;
    private float distance;
    public Animator zombieAnim;
    public float health;
    private BoxCollider bc;
    public bool isHit;
    public GameObject icon;
    private float shieldPower;
    private float damage, speed, healthPower;
    private int randPower, dayCount;
    private int characterIndex;
    private bool isDeadTime;
    private float x;
    private bool isFakeStandUp;
    private bool isRevival;
    public AudioSource boss_music;
    public AudioClip boss_sound;
    private bool isSound;
    void Start()
    {
        isFakeStandUp = true;
        isRevival = false;
        damage = speed = healthPower = 0;
        bc = GetComponent<BoxCollider>();
        health = Random.Range(200f, 300f);
        damage = 13f;
        agent = GetComponent<NavMeshAgent>();
        characterIndex = PlayerPrefs.GetInt("chooseCharacter");
        if (characterIndex == 1)
        {
            Target = GameObject.Find("newPlayer").GetComponent<player_controller>().transform;
        }
        else
        {
            Target = GameObject.Find("newPlayer2").GetComponent<player_controller>().transform;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f));
        shieldPower = PlayerPrefs.GetFloat("shieldPower");
        if (shieldPower == 0 || shieldPower == null)
        {
            shieldPower = 3f;
            PlayerPrefs.SetFloat("shieldPower", shieldPower);
        }
        randPower = Random.Range(0, 3);
        dayCount = PlayerPrefs.GetInt("currentDay");
        if (dayCount > 2 && dayCount < 41)
        {
            damage = (dayCount - 2) * 0.4f;
            speed = (dayCount - 2) * 0.15f;
            agent.speed += speed;
            healthPower = (dayCount - 2) * 2f;
            health += healthPower;
        }
        else if (dayCount >= 40)
        {
            damage = 40 * 0.4f;
            speed = 40 * 0.15f;
            agent.speed += speed;
            healthPower = 40 * 2f;
            health += healthPower;
        }
    }
    void Update()
    {
        if (health <= 0)
        {
            boss_music.Stop();
            agent.enabled = false;
            zombieAnim.SetBool("isDead", true);
            bc.enabled = false;
            icon.SetActive(false);
            return;
        }
        distance = Vector3.Distance(transform.position, Target.position);
        x = PlayerPrefs.GetFloat("energy");
        if (x <= 0)
        {
            boss_music.Stop();
            return;
        }
        if(isFakeStandUp == true)
        {
            if (((distance <= 15 && distance >= 4) || isHit == true) && x > 0)
            {
                if(isSound == false)
                {
                    isSound = true;
                    boss_music.PlayOneShot(boss_sound);
                }
                agent.enabled = true;
                agent.destination = Target.position;
                zombieAnim.SetBool("isStartAttack", false);
                zombieAnim.SetBool("isStartRun", true);
                if (distance <= 4)
                {
                    zombieAnim.SetBool("isStartAttack", true);
                    transform.LookAt(Target.position);
                }
            }
            else if (distance <= 4 && x > 0)
            {
                if (isSound == false)
                {
                    isSound = true;
                    boss_music.PlayOneShot(boss_sound);
                }
                zombieAnim.SetBool("isStartAttack", true);
                transform.LookAt(Target.position);
            }
            else
            {
                agent.enabled = false;
                zombieAnim.SetBool("isStartAttack", false);
                zombieAnim.SetBool("isStartRun", false);
            }
        }
    }
    public void fakeDeath()
    {
        if(isRevival == false)
        {
            int con = Random.Range(0, 2);
            if (con == 0)
            {
                isRevival = true;
                StartCoroutine(fakeStart());
            }
        }
    }
    IEnumerator fakeStart()
    {
        yield return new WaitForSeconds(3f);
        isFakeStandUp = false;
        zombieAnim.SetBool("isFake", true);
        zombieAnim.SetBool("isStartAttack", false);
        zombieAnim.SetBool("isStartRun", false);
        zombieAnim.SetBool("isDead", false);
        zombieAnim.SetBool("isHit", false);
        health = Random.Range(160f,200f);
        bc.enabled = true;
        icon.SetActive(true);
        agent.enabled = false;
        yield return new WaitForSeconds(2f);
        isFakeStandUp = true;
        agent.enabled = true;
        isSound = false;
        zombieAnim.SetBool("isFake", false);
        x = PlayerPrefs.GetFloat("energy");
        if (((distance <= 15 && distance >= 4) || isHit == true) && x > 0)
        {
            agent.enabled = true;
            agent.destination = Target.position;
            zombieAnim.SetBool("isStartAttack", false);
            zombieAnim.SetBool("isStartRun", true);
            if (distance <= 4)
            {
                zombieAnim.SetBool("isStartAttack", true);
                transform.LookAt(Target.position);
            }
        }
        else if (distance <= 4 && x > 0)
        {
            zombieAnim.SetBool("isStartAttack", true);
            transform.LookAt(Target.position);
        }
        else
        {
            agent.enabled = false;
            zombieAnim.SetBool("isStartAttack", false);
            zombieAnim.SetBool("isStartRun", false);
        }

    }
    public void retNormal()
    {
        zombieAnim.SetBool("isHit", false);
    }
    public void hitPlayer()
    {
        float x = PlayerPrefs.GetFloat("energy");
        if (x > 0)
        {
            x -= damage;
            PlayerPrefs.SetFloat("energy", x);
            if (characterIndex == 1)
            {
                GameObject.Find("newPlayer").GetComponent<player_controller>().healthBar();
            }
            else
            {
                GameObject.Find("newPlayer2").GetComponent<player_controller>().healthBar();
            }
        }
        else
        {
            agent.enabled = false;
            zombieAnim.SetBool("isStartAttack", false);
            zombieAnim.SetBool("isStartRun", false);
        }
    }
}
