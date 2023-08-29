using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Plant: MonoBehaviour
{
    public float healthPoint = 300;
	public float currentHP;
	public bool start;
	public string sortingLayerName = "Plant";
	protected Animator animator;
	protected new BoxCollider2D collider;
	protected virtual void Start()
	{
		currentHP = healthPoint;
		start = false;
		animator = GetComponent<Animator>();
		collider = GetComponent<BoxCollider2D>();
		animator.speed = 0;
		collider.enabled = false;
	}
	public virtual void setPlantStart()
	{
		start = true;
		animator.speed = 1;
		collider.enabled = true;
	}
	public virtual float ChangeHealth(float num)
	{
		currentHP = Mathf.Clamp(currentHP + num, 0, healthPoint);
		if(currentHP <= 0) GameObject.Destroy(gameObject);
		return currentHP;
	}
}
