using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchWood : Plant
{
    public GameObject FireBulletPrefab;
	protected override void Start()
	{
		base.Start();
		FireBulletPrefab = Resources.Load("Prefab/FireBullet") as GameObject;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		string tagName = collision.tag;
		if(tagName == "bullet")
		{
			if(collision.GetComponent<Bullet>().TorchWoodCreate)
				return;
			GameObject fireBullet = Instantiate(FireBulletPrefab, collision.transform.position, Quaternion.identity);
			fireBullet.GetComponent<Bullet>().TorchWoodCreate = true;
			Destroy(collision.gameObject);
		}
	}
}
