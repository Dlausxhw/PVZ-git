using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using Microsoft.Win32.SafeHandles;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerClickHandler
{
	public GameObject plantPrefab;
	private GameObject curGameObject;
	public PlantType plantType = PlantType.customize;
    public GameObject darkBg;
    public GameObject progressBar;
    public float interval;
	public int consume;
	private float timer;
	public PlantInfoItem plantInfo;
	public bool hasUse = false;
	public bool hasLock = false;
	public bool isMoving = false;
	public bool hasStart = false;
	private void Start()
	{
		if(darkBg == null)
			darkBg = transform.Find("dark").gameObject;
		if(progressBar == null)
			progressBar = transform.Find("progress").gameObject;
		darkBg.SetActive(false);
		progressBar.SetActive(false);
	}
	private void Update()
	{
        if(!GameManager.Instance.gameStart) return;
		if(!hasStart)
		{
			hasStart = true;
			darkBg.SetActive(true);
			progressBar.SetActive(true);
		}
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
		if(darkBg.activeSelf || !hasStart) return;
		PointerEventData pointerEventData = data as PointerEventData;
		curGameObject = Instantiate(plantPrefab);
		curGameObject.transform.position = TranslateScreenToWorld(pointerEventData.position);
		SoundManager.Instance.PlaySound(Globals.S_Seedlift);
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
				SoundManager.Instance.PlaySound(Globals.S_Plant);
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

	public void OnPointerClick(PointerEventData eventData)
	{
		if(isMoving) return;
		if(hasLock) return;
		if(hasUse) RemoveCard(gameObject);
		else AddCard();
	}
	public void RemoveCard(GameObject removeCard)
	{
		if(GameManager.Instance.gameStart || GameManager.Instance.LockedPlantCards) return;
		ChooseCardPanel chooseCardPanel = UIManager.Instance.chooseCardPanel;
		if(chooseCardPanel.ChooseCard.Contains(removeCard))
		{
			removeCard.GetComponent<Card>().isMoving = true;
			chooseCardPanel.ChooseCard.Remove(removeCard);
			chooseCardPanel.UpdateCardPosition();
			Transform cardParent = UIManager.Instance.allCardPanel.Bg.transform.Find("Card" + removeCard.GetComponent<Card>().plantInfo.plantId);
			Vector3 curPosition = removeCard.transform.position;
			removeCard.transform.SetParent(UIManager.Instance.transform, false);
			removeCard.transform.position = curPosition;
			removeCard.transform.DOMove(cardParent.position, 0.3f).OnComplete(
				() =>
				{
					cardParent.Find("BeforeCard").GetComponent<Card>().hasLock = false;
					cardParent.Find("BeforeCard").GetComponent<Card>().darkBg.SetActive(false);
					removeCard.GetComponent<Card>().isMoving = false;
					Destroy(removeCard);
				});
		}
	} 
	public void AddCard()
	{
		ChooseCardPanel chooseCardPanel = UIManager.Instance.chooseCardPanel;
		int curIndex = chooseCardPanel.ChooseCard.Count;
		if(curIndex >= chooseCardPanel.cardLength) return;
		GameObject useCard = Instantiate(plantInfo.cardPrefab);
		useCard.transform.SetParent(UIManager.Instance.transform);
		useCard.transform.position = transform.position;
		useCard.name = "Card";
		useCard.GetComponent<Card>().plantInfo = plantInfo;
		hasLock = true;
		darkBg.SetActive(true);
		Transform targetObject = chooseCardPanel.cards.transform.Find("Card" + curIndex);
		useCard.GetComponent<Card>().isMoving = true;
		useCard.GetComponent<Card>().hasUse = true;
		chooseCardPanel.ChooseCard.Add(useCard);
		SoundManager.Instance.PlaySound(Globals.S_Seedlift);
		useCard.transform.DOMove(targetObject.position, 0.3f).OnComplete(
			() =>
			{
				useCard.transform.SetParent(targetObject, false);
				useCard.transform.localPosition = Vector3.zero;
				useCard.GetComponent<Card>().isMoving = false;
			});
	}
}
