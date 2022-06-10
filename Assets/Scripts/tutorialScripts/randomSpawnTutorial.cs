using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawnTutorial : MonoBehaviour
{
    public GameObject ammoCase;
    public GameObject zombie;
    private Vector3 spawnPosition;
    private bool isCreate = false;
    public bool canCreate = false;
    void Update()
    {
        if(canCreate == false)
        {
            return;
        }
        if(isCreate == false)
        {
            isCreate = true;
            for (int i = 0; i < Random.Range(10, 20); i++)
            {
                spawnPosition = new Vector3(Random.Range(-35f, 68f), 1.75f, Random.Range(-35f, 55f));
                Instantiate(zombie, spawnPosition, Quaternion.identity);
            }
        }
    }
}
