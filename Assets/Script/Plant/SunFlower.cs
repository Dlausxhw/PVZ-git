using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SunFlower : Plant
{
    public float interval = 23.5f;
	[ReadOnly][SerializeField] private float timer = 0;
	public int GenerationQuantity = 1;
	private int SunNum = 0;
	public GameObject SunType;
	protected override void Start()
	{
		base.Start();
		if(animator == null)
			animator = GetComponent<Animator>();
		timer = interval - 10f;
	}
	private void Update()
	{
		if(!start) return;
		if((timer = timer + Time.deltaTime) >= interval)
			animator.SetBool("SunLight", true);
	}
	private void GenerateSunlight()
	{
		timer = 0;
		animator.SetBool("SunLight", false);
		GenerateSunLightRecursion(GenerationQuantity);
	}
	private void GenerateSunLightRecursion(int generationQuatity)
	{
		generationQuatity--;
		GameObject Sun = Instantiate(SunType);
		float randomX, randomY;
		if (++SunNum % 2 == 1)
			randomX = Random.Range(transform.position.x - 30, transform.position.x - 20);
		else
			randomX = Random.Range(transform.position.x + 20, transform.position.x + 30);
		randomY = Random.Range(transform.position.y - 20, transform.position.y + 20);
		Sun.transform.position = new Vector3(randomX, randomY, -1);
		Sun.transform.DOScale(0, 0.5f).From();
		Sun.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).From();
		if(generationQuatity > 0) GenerateSunLightRecursion(generationQuatity);
	}
}
