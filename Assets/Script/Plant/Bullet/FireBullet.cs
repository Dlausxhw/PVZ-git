using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : Bullet
{
	protected override void Start()
	{
		base.Start();
		damage = damage * 2;
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if(Hited) return;
		if(collision.tag == "zombie")
		{
			Zombie zombie = collision.GetComponent<Zombie>();
			if(!zombie.isDead)
			{
				// Set it to Fire hit
				transform.Find("FireHit").gameObject.SetActive(true);
				zombie.ChangeHealth(-damage);
				StartDestory();
			}
		}
	}
	protected override void StartDestory()
	{
		base.StartDestory();
		SoundManager.Instance.PlaySound(Globals.S_Fire_Pea);
	}
}
