using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public enum State
{
	None,
	Down,
	Up,
	Over
}
public class Squash : Plant
{
    public float findZombieDistances = 125;
    public int SelfLine;
	public int damage = 1800;
	public int JumpPower = 75;
	public int JumpUpSpeed = 1200;
	public int JumpDownSpeed = 1800;
	private Vector2 AttackPosition;
	private State curState = State.None;

	public override void setPlantStart()
	{
		base.setPlantStart();
		SelfLine = GameManager.Instance.getPlantLine(gameObject);
		collider.enabled = false;
		InvokeRepeating("CheckZombieInRange", 1, 0.5f);
	}
	public override float ChangeHealth(float num)
	{
		return 0f;
	}
	private void Update()
	{
		switch (curState)
		{
			case State.Down:
				break;
			case State.Up:
				transform.position = Vector2.MoveTowards(transform.position, new Vector2(AttackPosition.x, AttackPosition.y + JumpPower), Time.deltaTime * JumpUpSpeed);
				break;
			case State.Over:
				transform.position = Vector2.MoveTowards(transform.position, AttackPosition, Time.deltaTime * JumpDownSpeed);
				break;
			default:
				break;
		}
	}
	public void CheckZombieInRange()
	{
		if(AttackPosition != Vector2.zero) return;
		List<GameObject> zombies = GameManager.Instance.getLineZombies(SelfLine);
		if(zombies.Count <= 0 ) return;
		float minDistence = findZombieDistances;
		GameObject nearZombie = null;
		for(int i = 0; i < zombies.Count; i++)
		{
			GameObject Zombie = zombies[i];
			float dis = Vector2.Distance(gameObject.transform.position, Zombie.transform.position);
			if(dis < minDistence)
			{
				minDistence = dis;
				nearZombie = Zombie;
			}
		}
		if(nearZombie == null || nearZombie.GetComponent<Zombie>().isDead) return;
		AttackPosition = nearZombie.transform.position;
		DoSquashLook();
	}
	public void DoSquashLook()
	{
		string LookName = "right";
		if(AttackPosition.x < transform.position.x)
			LookName = "left";
		animator.SetTrigger(LookName);
	}
	public void SetAttackUp()
	{
		curState = State.Up;
		animator.SetTrigger("attackUp");
	}
	public void SetAttackOver()
	{
		curState = State.Over;
		animator.SetTrigger("attackOver");
	}
	public void DoReallyAttack()
	{
		collider.enabled = true;
		Destroy(gameObject, 0.5f);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "zombie")
		{
			collision.GetComponent<Zombie>().ChangeHealth(-damage);
		}
	}
}
