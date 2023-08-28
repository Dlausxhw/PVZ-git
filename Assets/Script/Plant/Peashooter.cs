using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;


public class Peashooter : Plant
{
    public float interval = 1.35f;
    public GameObject PeaBullet;
    public Transform BulletPos;
    private float timer = 0;
    private bool canShoot = false;

    public void CheckCanShoot()
    {
		char[] chars = transform.parent.transform.parent.name.ToCharArray();
		chars[0] = char.ToUpper(chars[0]);
		Type type = GameManager.Instance.globals.GetType();
		FieldInfo property = type.GetField(new string(chars) + "Zombie");
		if(property != null)
			canShoot = (int)property.GetValue(GameManager.Instance.globals) != 0;
	}
	void Update()
    {
        if(!start) return;
		CheckCanShoot();
		if(canShoot && (timer = timer + Time.deltaTime) >= interval) {
            timer = 0; Shoot();
        }
    }
    public void Shoot()
    {
		SoundManager.Instance.PlaySoundTimeCallback(Globals.S_Shoot, 0.45f, () => Instantiate(PeaBullet, BulletPos.position, Quaternion.identity), 0.5f);
	}
}
