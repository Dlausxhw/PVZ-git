using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHead : MonoBehaviour
{
    public void HeadAnimatorOver() {
		gameObject.transform.parent = null;
		GameObject.Destroy(gameObject, 1.25f);
	}
}
