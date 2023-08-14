using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
	private float cracked1HP = 2666;
	private float cracked2HP = 1333;
	private bool cracked1 = false;
	private bool cracked2 = false;
	protected override void Start()
	{
		healthPoint = 4000;
		base.Start();
	}
	public override float ChangeHealth(float num)
	{
		currentHP = Mathf.Clamp(currentHP + num, 0, healthPoint);
		if(currentHP <= cracked1HP && !cracked1) {
			animator.ResetTrigger("cracked");
			animator.SetTrigger("cracked");
			cracked1 = true;
		}
		if(currentHP <= cracked2HP && !cracked2) {
			animator.ResetTrigger("cracked");
			animator.SetTrigger("cracked");
			cracked2 = true;
		}
		if (currentHP <= 0) GameObject.Destroy(gameObject);
		return currentHP;
	}
}
