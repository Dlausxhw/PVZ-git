using functions;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public Direction direction = Direction.Left;
    public float Speed = 20;
    protected bool isWalk = true;
    protected Animator animator;
    public float damageInterval = 0.25f;
    public float damage = 20;
    protected float damageTimer = 0;
    public float healthPoint = 300;
    [HideInInspector] public float currentHP;
    public bool isDead = false;
	protected new SpriteRenderer renderer;


	protected virtual void Start()
	{
		currentHP = healthPoint;
        animator = GetComponent<Animator>();
		renderer = GetComponent<SpriteRenderer>();
	}
	protected virtual void Update()
	{
		if(isDead) return;
		transform.position += Move();
	}

	protected Vector3 Move()
    {
        if(isWalk) 
            return direction.vector3() * Speed * Time.deltaTime;
        return Vector3.zero;
    }
	public virtual void DieAnimatorOver()
	{
		animator.enabled = false;
		GameManager.Instance.ZombieDead(gameObject);
		GameObject.Destroy(gameObject, 1.25f);
	}
	public virtual float ChangeHealth(float damage)
	{
		if(isDead) return 0f;
		GetComponent<BossMouseFlash>().FlashWhite(0.45f);
		Invoke("StopWhiteFlash", 0.1f);
		currentHP = Mathf.Clamp(currentHP + damage, 0, healthPoint);
		if (currentHP <= 0)
		{
			animator.SetTrigger("die");
			isDead = true;
		}
		return currentHP;
	}
	void StopWhiteFlash() => GetComponent<BossMouseFlash>().ResetFlash();
}
