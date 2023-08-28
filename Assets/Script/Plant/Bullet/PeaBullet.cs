using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class PeaBullet: Bullet
{
	protected override void Start()
	{
		base.Start();
		transparency = true;
	}
	protected override void StartDestory()
	{
		base.StartDestory();
		SoundManager.Instance.PlaySound(Globals.S_PeaHit, 1.5f);
	}
}
