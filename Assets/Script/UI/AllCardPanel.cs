using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class AllCardPanel : MonoBehaviour
{
    public GameObject Bg;
    public GameObject CardPrefab;
    public int CardCount = 40;
    public static AllCardPanel Instance;
    private float upPosY = 80;
    private float downPosY = -450;
	private void Awake()
	{
		Instance = this;
	}
	// Start is called before the first frame update
	void Start()
    {
        for(int i = 0; i < CardCount; i++)
        {
            GameObject BeforeCard = Instantiate(CardPrefab);
            BeforeCard.transform.SetParent(Bg.transform, false);
            BeforeCard.name = "Card" + i.ToString();
        }
    }
    public Tweener panelUp()
    {
        return gameObject.transform.DOMoveY(upPosY, 1f);
    }
    public Tweener panelDown()
    {
        return gameObject.transform.DOMoveY(downPosY, 1f);
	}
    public Tweener startPanelPreview()
    {
        return gameObject.transform.DOMoveX(transform.position.x + CameraController.previewPosX, CameraController.duration);
    }
    public Tweener endPanelPreview()
    {
        return gameObject.transform.DOMoveX(transform.position.x -  CameraController.previewPosX, CameraController.duration);
    }
    public void InitCards()
    {
        foreach(PlantInfoItem plantInfo in GameManager.Instance.plantInfo.PlantInfoList)
        {
            Transform cardParent = Bg.transform.Find("Card" + plantInfo.plantId);
            GameObject reallyCard = Instantiate(plantInfo.cardPrefab) as GameObject;
            reallyCard.GetComponent<Card>().plantInfo = plantInfo;
            reallyCard.transform.SetParent(cardParent, false);
            reallyCard.transform.localPosition = Vector2.zero;
            reallyCard.name = "BeforeCard";
        }
    }
    public void OnBtnStart()
    {
        panelDown();
        CameraController.Instance.endPreview().OnComplete(
            () => { GameManager.Instance.GameReallyStart(); }
            );
	}
}
