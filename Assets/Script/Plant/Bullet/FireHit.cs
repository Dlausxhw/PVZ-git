using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHit : MonoBehaviour
{
	private int damage;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision != null && collision.tag == "zombie")
		{
			Zombie zombie = collision.GetComponent<Zombie>();
			damage = (int)transform.parent.GetComponent<Bullet>().damage / 2;
			if(!zombie.isDead)
			{
				zombie.ChangeHealth(-damage);
				Destroy(gameObject);
			}
		}
	}
}
