using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawn : MonoBehaviour
{
    public GameObject ammoCase;
    public GameObject[] zombies;
    public GameObject[] bosses;
    private Vector3 spawnPosition;
    private int spawnCheater;
    void Start()
    {
        for (int i = 0; i < Random.Range(8,13); i++)
        {
            spawnPosition = new Vector3(Random.Range(-25f, 60f), 2f, Random.Range(-35f, 55f));
            Instantiate(ammoCase, spawnPosition, Quaternion.identity);
        }
        for (int i = 0; i < Random.Range(8, 13); i++)
        {
            int k = Random.Range(0, zombies.Length);
            float y;
            if (spawnCheater < Random.Range(1,3))
            {
                if (k == 2)
                {
                    y = 1.86f;
                    spawnCheater++;
                }
                else
                {
                    y = 1.75f;
                }
            }
            else
            {
                y = 1.75f;
                if(Random.Range(0,2) == 0)
                {
                    if(k == 2)
                    {
                        k--;
                    }
                }
                else
                {
                    if (k == 2)
                    {
                        k -= 2;
                    }
                }
            }
            spawnPosition = new Vector3(Random.Range(-25f, 60f), y, Random.Range(-35f, 55f));
            Instantiate(zombies[k], spawnPosition, Quaternion.identity);
        }
        if(Random.Range(0,2) == 0)
        {
            int k = Random.Range(0, bosses.Length);
            float y;
            if (k == 1)
            {
                y = -4f;
                spawnPosition = new Vector3(Random.Range(-25f, 60f), y, Random.Range(-35f, 55f));
                StartCoroutine(spawnBoss2(spawnPosition, k));
            }
            else
            {
                y = 0.5f;
                spawnPosition = new Vector3(Random.Range(-25f, 60f), y, Random.Range(-35f, 55f));
                Instantiate(bosses[k], spawnPosition, Quaternion.identity);
            }
        }
    }
    IEnumerator spawnBoss2(Vector3 x, int index)
    {
        yield return new WaitForSeconds(Random.Range(30f, 40f));
        Instantiate(bosses[index], spawnPosition, Quaternion.identity);
    }
}
