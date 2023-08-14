using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Security.Cryptography;



public class Card : MonoBehaviour
{
	public GameObject plantPrefab;
	private GameObject curGameObject;
	public PlantType plantType = PlantType.customize;
    public GameObject darkBg;
    public GameObject progressBar;
    public float interval;
	public int consume;
	private float timer;
	private void Start()
	{
		if(darkBg == null)
			darkBg = transform.Find("dark").gameObject;
		if(progressBar == null)
			progressBar = transform.Find("progress").gameObject;
	}
	private void Update()
	{
		timer += Time.deltaTime;
		UpdateProgress();
		UpdateDarkBg();
	}
	private void UpdateProgress() 
	{
		float per = Mathf.Clamp(timer / interval, 0, 1);
		progressBar.GetComponent<Image>().fillAmount = 1 - per;
	}
	private void UpdateDarkBg()
	{
		if(progressBar.GetComponent<Image>().fillAmount == 0 && GameManager.Instance.SunNumber >= consume)
			darkBg.SetActive(false);
		else
			darkBg.SetActive(true);
	}
	public void OnBeginDrag(BaseEventData data)
	{
		if(darkBg.activeSelf) return;
		PointerEventData pointerEventData = data as PointerEventData;
		curGameObject = Instantiate(plantPrefab);
		curGameObject.transform.position = TranslateScreenToWorld(pointerEventData.position);
	}
	public void OnDraging(BaseEventData data)
	{
		if(curGameObject == null) return;
		PointerEventData pointerEventData = data as PointerEventData;
		curGameObject.transform.position = TranslateScreenToWorld(pointerEventData.position);
	}
	public void OnEndDrag(BaseEventData data)
	{
		if(curGameObject == null) return;
		PointerEventData pointerEventData = data as PointerEventData;
		Collider2D[] colliders = Physics2D.OverlapPointAll(TranslateScreenToWorld(pointerEventData.position));
		foreach(Collider2D collider in colliders) {
			if(collider.tag == "Land" && collider.transform.childCount == 0)
			{
				curGameObject.transform.parent = collider.transform;
				curGameObject.GetComponent<Plant>().setPlantStart();
				curGameObject.transform.localPosition = Vector3.zero;
				curGameObject = null;
				GameManager.Instance.ChangeSunNumber(-consume);
				timer = 0;
				break;
			}
		}
		if(curGameObject != null) {
			GameObject.Destroy(curGameObject);
			curGameObject = null;
		}
	}
	public static Vector3 TranslateScreenToWorld(Vector3 position)
	{
		Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
		return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0);
	}
}
