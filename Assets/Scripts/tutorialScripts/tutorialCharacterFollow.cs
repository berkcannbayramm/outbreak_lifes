using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tutorialCharacterFollow : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Target;
    private float distance;
    public Animator tutorialCharacterAnim;
    public float health;
    public bool isHit;
    void Start()
    {
        health = 9f;
        agent = GetComponent<NavMeshAgent>();
        Target = GameObject.Find("newPlayer").GetComponent<player_controller_tutorial>().transform;
    }
    void Update()
    {
        if (health <= 0)
        {
            agent.enabled = false;
            tutorialCharacterAnim.SetBool("isDead", true);
            return;
        }
        distance = Vector3.Distance(transform.position, Target.position);
        if ((distance <= 15 && distance >= 4))
        {
            agent.enabled = true;
            agent.destination = Target.position;
            tutorialCharacterAnim.SetBool("isRun", true);
            if (distance <= 2.5f)
            {
                tutorialCharacterAnim.SetBool("isRun", false);
            }
        }
        else if (distance <= 2.5f)
        {
            tutorialCharacterAnim.SetBool("isRun", false);
        }
    }
}
