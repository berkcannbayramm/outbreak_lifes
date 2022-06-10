using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class boss_2_follow : MonoBehaviour
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
    private bool isClimbedBoss;
    public ParticleSystem ps;
    public AudioSource boss_music;
    public AudioClip boss_sound;
    private bool isSound;
    void Start()
    {
        damage = speed = healthPower = 0;
        bc = GetComponent<BoxCollider>();
        health = Random.Range(180f, 320f);
        damage = 13f;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
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
        if(isClimbedBoss == true)
        {
            if (health <= 0)
            {
                boss_music.Stop();
                agent.enabled = false;
                zombieAnim.SetBool("isDead", true);
                Destroy(icon);
                Destroy(bc);
                return;
            }
            distance = Vector3.Distance(transform.position, Target.position);
            float x = PlayerPrefs.GetFloat("energy");
            if (x <= 0)
            {
                boss_music.Stop();
                return;
            }
            if (((distance <= 15 && distance >= 4) || isHit == true) && x > 0)
            {
                if (isSound == false)
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
            }
            else
            {
                boss_music.Stop();
                agent.enabled = false;
                zombieAnim.SetBool("isStartAttack", false);
                zombieAnim.SetBool("isStartRun", false);
            }
        }
    }
    public void climbed()
    {
        zombieAnim.SetBool("isClimbed", true);
        isClimbedBoss = true;
        ps.Stop();
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
            x -= (shieldPower + damage);
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
