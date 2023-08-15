using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSun : MonoBehaviour
{
    public float interval;
	private float timer = 0;
    public int value = 25;
    public float duration = 5;
	[ReadOnly][SerializeField] private new SpriteRenderer renderer;
	public Vector3 targetPosition = Vector3.zero;
	private bool clicked = false;
	private void Start()
	{
		renderer = GetComponent<SpriteRenderer>();
	}
	public void SetTargetPos(Vector3 position)
	{
		targetPosition = position;
	}
	private void Update()
	{
		if(targetPosition != Vector3.zero && Vector3.Distance(transform.position, targetPosition) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetPosition, 0.1f);
			return;
		}
		timer += Time.deltaTime;
		if(timer >= duration)
			StartDestory();
	}
	private void StartDestory()
	{
		Color spriteColor = renderer.color;
		spriteColor.a = 1 - (timer - duration);
		renderer.color = spriteColor;
		if(spriteColor.a <= 0) Destroy(gameObject);
	}
	private void OnMouseDown()
	{
		GameManager.Instance.ChangeSunNumber(value);
		clicked = true;
		SoundManager.Instance.PlaySound(Globals.S_SunCollect);
		GameObject.Destroy(gameObject);
	}
}
