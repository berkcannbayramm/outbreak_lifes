using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutZombie : MonoBehaviour
{
    public Animator zombieAnim;
    public float health;
    public GameObject icon;
    void Start()
    {
        health = 80f;
    }
    void Update()
    {
        if (health <= 0)
        {
            zombieAnim.SetBool("isDead", true);
            Destroy(icon);
            return;
        }
    }
}
