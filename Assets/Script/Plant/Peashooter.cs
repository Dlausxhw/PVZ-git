using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
		PropertyInfo property = type.GetProperty(new string(chars) + "Zombie");
		if(property != null && property.CanWrite)
			canShoot = (int)property.GetValue(GameManager.Instance.globals) != 0;
        Debug.Log((int)property.GetValue(GameManager.Instance.globals));
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
		Instantiate(PeaBullet, BulletPos.position, Quaternion.identity);
	}
}
