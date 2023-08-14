using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    public Direction direction = Direction.Right;
    public float speed = 150;
    public float damage = 25;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected float DestoryTimer = 0;
    protected bool Hited = false;
    protected bool transparency = false;
	public bool TorchWoodCreate = false;

	protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected virtual void Update()
    {
        if(!Hited)
            transform.position += direction.vector3() * speed * Time.deltaTime;
        if(transform.position.x > 535)
            GameObject.Destroy(gameObject);
		if(Hited && transparency) Destorying();
	}
	protected virtual void StartDestory()
	{
		animator.ResetTrigger("hit");
		animator.SetTrigger("hit");
		Hited = true;
	}
	protected virtual void Destorying()
	{
		Color spriteColor = spriteRenderer.color;
		DestoryTimer += Time.deltaTime * 2;
		spriteColor.a = 1 - DestoryTimer;
		if(spriteColor.a <= 0) GameObject.Destroy(gameObject);
		spriteRenderer.color = spriteColor;
	}
	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if(Hited) return;
		if(collision.tag == "zombie")
		{
			Zombie zombie = collision.GetComponent<Zombie>();
			if (!zombie.isDead)
			{
				zombie.ChangeHealth(-damage);
				StartDestory();
			}
		}
	}
	public virtual void DestoryBullet()
    {
        GameObject.Destroy(gameObject);
    }
}
