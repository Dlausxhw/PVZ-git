using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class ProgressPanel : MonoBehaviour
{
	private GameObject Bg;
	private GameObject Progress;
	private GameObject LevelText;
	private GameObject Head;
	private GameObject Flag;
	private GameObject FlagPrefab;
	private RectTransform BgRectTransform;
	private RectTransform HeadRectTransform;
	private float offset = 0f;
	private void Start()
	{
		this.Bg = transform.Find("Bg").gameObject;
		this.Progress = transform.Find("Progress").gameObject;
		this.LevelText = transform.Find("LevelText").gameObject;
		this.Head = transform.Find("Head").gameObject;
		this.Flag = transform.Find("Flag").gameObject;
		FlagPrefab = Resources.Load("Prefab/Flag") as GameObject;
		BgRectTransform = Bg.GetComponent<RectTransform>();
		HeadRectTransform = Head.GetComponent<RectTransform>();
	}

	public void SetPercent(float per)
	{
		Progress.GetComponent<Image>().fillAmount = per;
		float originPosX = BgRectTransform.position.x + BgRectTransform.sizeDelta.x / 2;
		float width = BgRectTransform.sizeDelta.x;
		HeadRectTransform.position = new Vector2(originPosX - per * width + offset, HeadRectTransform.position.y);
	}
	public void SetFlagPercent(float per)
	{
		if(per == 1) return;
		Flag.SetActive(false);
		float originPosX = BgRectTransform.position.x + BgRectTransform.sizeDelta.x / 2;
		float width = BgRectTransform.sizeDelta.x;
		GameObject newFlag = Instantiate(FlagPrefab);
		newFlag.SetActive(true);
		newFlag.transform.SetParent(gameObject.transform, false);
		var newFlagRTransform = newFlag.GetComponent<RectTransform>();
		// newFlagRTransform.position = Flag.GetComponent<RectTransform>().position;
		newFlagRTransform.position = new Vector2(originPosX - per * width + offset, newFlagRTransform.position.y);
		Head.transform.SetAsLastSibling();
	}
}
