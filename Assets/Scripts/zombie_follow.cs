using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie_follow : MonoBehaviour
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
    public string zombieName;
    void Start()
    {
        damage = speed = healthPower = 0;
        bc = GetComponent<BoxCollider>();
        health = Random.Range(30f, 80f);
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
        if(shieldPower == 0 || shieldPower == null)
        {
            shieldPower = 3f;
            PlayerPrefs.SetFloat("shieldPower", shieldPower);
        }
        randPower = Random.Range(0, 3);
        dayCount = PlayerPrefs.GetInt("currentDay");
        if (dayCount > 2 && dayCount < 41)
        {
            if (randPower == 0)
            {
                damage = (dayCount - 2) * 0.2f;
            }
            else if (randPower == 1)
            {
                speed = (dayCount - 2) * 0.05f;
                agent.speed += speed;
            }
            else
            {
                healthPower = (dayCount - 2) * 2f;
                health += healthPower;
            }
        }
        else if (dayCount >= 40)
        {
            if (randPower == 0)
            {
                damage = 40 * 0.4f;
            }
            else if (randPower == 1)
            {
                speed = 40 * 0.05f;
                agent.speed += speed;
            }
            else
            {
                healthPower = 40 * 2f;
                health += healthPower;
            }
        }
    }
    void Update()
    {
        if (health <=0)
        {
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
            return;
        }
        if (((distance <= 15 && distance>=4) || isHit == true) && x>0)
        {
            agent.enabled = true;
            agent.destination = Target.position;
            zombieAnim.SetBool("isStartAttack", false);
            zombieAnim.SetBool("isStartRun", true);
            if (distance <= 4)
            {
                zombieAnim.SetBool("isStartAttack", true);
                if (zombieName != "zombie_3")
                {
                    transform.LookAt(Target.position);
                }
            }
        }
        else if(distance <= 4 && x > 0)
        {
            zombieAnim.SetBool("isStartAttack", true);
            if (zombieName != "zombie_3")
            {
                transform.LookAt(Target.position);
            }
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
        if(x> 0)
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "cube")
        {
            transform.position = new Vector3(Random.Range(-25f, 60f), 2f, Random.Range(-35f, 55f));
        }
    }
}
