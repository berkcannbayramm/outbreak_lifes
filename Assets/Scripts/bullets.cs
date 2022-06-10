using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bullets : MonoBehaviour
{
    private float weaponDamagePower;
    void Start()
    {
        weaponDamagePower = PlayerPrefs.GetFloat("weaponPower");
        if(weaponDamagePower == 0 || weaponDamagePower == null)
        {
            weaponDamagePower = 15f;
        }
    }
    void Update()
    {
        transform.Translate(Vector3.forward*1.5f);
        StartCoroutine(collisionBullet());
    }
    IEnumerator collisionBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "zombie")
        {
            if (other.gameObject.GetComponent<zombie_follow>().health > 0)
            {
                other.gameObject.GetComponent<zombie_follow>().GetComponent<Animator>().SetBool("isHit", true);
                other.gameObject.GetComponent<zombie_follow>().health -= weaponDamagePower;
                other.gameObject.GetComponent<zombie_follow>().GetComponent<NavMeshAgent>().enabled = false;
                other.gameObject.GetComponent<zombie_follow>().isHit = true;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "boss")
        {
            if (other.gameObject.GetComponent<boss_follow>().health > 0)
            {
                other.gameObject.GetComponent<boss_follow>().GetComponent<Animator>().SetBool("isHit", true);
                other.gameObject.GetComponent<boss_follow>().health -= weaponDamagePower;
                other.gameObject.GetComponent<boss_follow>().GetComponent<NavMeshAgent>().enabled = false;
                other.gameObject.GetComponent<boss_follow>().isHit = true;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "boss_2")
        {
            if (other.gameObject.GetComponent<boss_2_follow>().health > 0)
            {
                other.gameObject.GetComponent<boss_2_follow>().GetComponent<Animator>().SetBool("isHit", true);
                other.gameObject.GetComponent<boss_2_follow>().health -= weaponDamagePower;
                other.gameObject.GetComponent<boss_2_follow>().GetComponent<NavMeshAgent>().enabled = false;
                other.gameObject.GetComponent<boss_2_follow>().isHit = true;
            }
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "tutZombie")
        {
            if (other.gameObject.GetComponent<tutZombie>().health > 0)
            {
                other.gameObject.GetComponent<tutZombie>().GetComponent<Animator>().SetBool("isHit", true);
                other.gameObject.GetComponent<tutZombie>().health -= weaponDamagePower;
            }
            Destroy(gameObject);
        }
    }
}
