using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCardPanel : MonoBehaviour
{
	public static ChooseCardPanel Instance;
	public static float downPos = 18f;
	public static float upPos = 95f;
	public static float originPosX;
	public static float originPosY;
    public GameObject cards;
    public GameObject beforeCardPrefab;
    public List<GameObject> ChooseCard;
	public int cardLength = 8;
	private void Awake()
	{
		Instance = this;
	}
	private void Start()
	{
		originPosX = transform.position.x;
		originPosY = transform.position.y;
		ChooseCard = new List<GameObject>();
		for(int i = 0; i < cardLength; i++)
		{
			GameObject beforeCard = Instantiate(beforeCardPrefab);
			beforeCard.transform.SetParent(cards.transform, false);
			beforeCard.name = "Card" + i.ToString();
			beforeCard.transform.Find("Bg").gameObject.SetActive(false);
		}
	}
	public void UpdateCardPosition()
	{
		for(int i = 0; i < ChooseCard.Count; i++)
		{
			GameObject useCard = ChooseCard[i];
			Transform targetObject = cards.transform.Find("Card" + i.ToString());
			useCard.GetComponent<Card>().isMoving = true;
			useCard.transform.DOMove(targetObject.position, 0.3f).OnComplete(
				() =>
				{
					useCard.transform.SetParent(targetObject, false);
					useCard.transform.localPosition = Vector3.zero;
					useCard.GetComponent<Card>().isMoving = false;
				});
		}
	}
	public Tweener panelUp()
	{
		return gameObject.transform.DOMoveY(transform.position.y + 95, 0);
	}
	public Tweener panelDown()
	{
		return gameObject.transform.DOMoveY(originPosY, CameraController.duration);
	}
	public Tweener startPanelPreview()
	{
		return gameObject.transform.DOMoveX(transform.position.x + CameraController.previewPosX, CameraController.duration);
	}
	public Tweener endPanelPreview()
	{
		return gameObject.transform.DOMoveX(originPosX, CameraController.duration);
	}
}
