using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equipment : MonoBehaviour
{
    private string[] things = { "apple", "soup", "water", "pill", "ammo"};
    public string selected_thing;
    private int dayCount;
    private bool isValued;
    public Animator coverAnim;
    public Light light;
    private bool isCreate;
    public void Start()
    {
        transform.rotation = Quaternion.Euler(-90f, 0f, Random.Range(0f,360f));
        dayCount = PlayerPrefs.GetInt("currentDay");
        int pillCount = PlayerPrefs.GetInt("pillCount");
        
        if(dayCount > 2)
        {
            selected_thing = things[Random.Range(0, things.Length)];
        }
        else
        {
            if(Random.Range(0,2) == 0)
            {
                selected_thing = "soup";
            }
            else
            {
                selected_thing = "ammo";
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ground")
        {
            StartCoroutine(destroyRB());
        }
        if(collision.gameObject.tag != "ground" && collision.gameObject.tag != "Player")
        {
            transform.position = new Vector3(Random.Range(-25f, 60f), 1.5f, Random.Range(-35f, 55f));
        }
    }
    IEnumerator destroyRB()
    {
        yield return new WaitForSeconds(2f);
        Destroy(GetComponent<Rigidbody>());
    }
}
