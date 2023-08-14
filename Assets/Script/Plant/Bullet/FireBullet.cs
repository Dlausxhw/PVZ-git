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
	protected override void StartDestory()
	{
		base.StartDestory();
		animator.speed = 2;
	}
}
