using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_camera : MonoBehaviour
{
    private float camSpeed = 0.5f;
    private float camVelocity;
    private GameObject target;
    private bool isValued;
    private void LateUpdate()
    {
        if(isValued == false)
        {
            isValued = true;
            int characterIndex = PlayerPrefs.GetInt("chooseCharacter");
            if (characterIndex == 1)
            {
                target = GameObject.Find("newPlayer");
            }
            else if (characterIndex == 2)
            {
                target = GameObject.Find("newPlayer2");
            }
        } 
        var CameraNewPos = transform.position;
        transform.position = new Vector3(Mathf.SmoothDamp(CameraNewPos.x, target.transform.position.x, ref camVelocity, camSpeed), target.transform.position.y + 10f, target.transform.position.z -22f);
    }
}
