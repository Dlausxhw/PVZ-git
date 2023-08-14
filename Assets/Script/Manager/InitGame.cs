using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    private bool Initializtion = false;
    void Start()
    {
        if(Initializtion) {
            GameObject MainCamera = GameObject.Find("MainCamera");
			GameObject ground = GameObject.Find("background");
			MainCamera.transform.position = new Vector3(0, 0, -10);
			ground.transform.position = new Vector2(167, 0);
		}
    }
}
