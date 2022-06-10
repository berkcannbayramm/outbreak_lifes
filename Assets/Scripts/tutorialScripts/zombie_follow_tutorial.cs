using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class zombie_follow_tutorial : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Target;
    private float distance;
    public Animator zombieAnim;
    public float health;
    private BoxCollider bc;
    public bool isHit;
    public GameObject icon;
    private float x;
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        health = Random.Range(30f, 80f);
        agent = GetComponent<NavMeshAgent>();
        Target = GameObject.Find("Cube").GetComponent<tutorialCharacterFollow>().transform;
        transform.rotation = Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f));
        x = GameObject.Find("Cube").GetComponent<tutorialCharacterFollow>().health;
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
                //transform.LookAt(Target.position);
            }
        }
        else if(distance <= 4 && x > 0)
        {
            zombieAnim.SetBool("isStartAttack", true);
            //transform.LookAt(Target.position);
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
        x = GameObject.Find("Cube").GetComponent<tutorialCharacterFollow>().health;
        if(x> 0)
        {
            x -= 3f;
        }
        else
        {
            agent.enabled = false;
            zombieAnim.SetBool("isStartAttack", false);
            zombieAnim.SetBool("isStartRun", false);
        }
        GameObject.Find("Cube").GetComponent<tutorialCharacterFollow>().health = x;
    }
}
