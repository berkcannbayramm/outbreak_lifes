using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy : MonoBehaviour
{
    public float energyCount;
    void Start()
    {
        //PlayerPrefs.SetFloat("energy", 0);
        if (PlayerPrefs.GetFloat("energy") == null || PlayerPrefs.GetFloat("energy") == 0)
        {
            energyCount = 100;
            PlayerPrefs.SetFloat("energy", energyCount);
        }
        else
        {
            energyCount = PlayerPrefs.GetFloat("energy");
        }
    }
}
