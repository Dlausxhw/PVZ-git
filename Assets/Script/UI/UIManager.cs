using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	public Text sumNumberText;
	public ProgressPanel progressPanel;
	private void Start()
	{
		Instance = this;
		GameObject Cards = transform.Find("ChooseCardPanel").transform.Find("Cards").gameObject;
		for(int i = 0; i < Cards.transform.childCount; i++)
		{
			GameObject childGameObject = Cards.transform.GetChild(i).gameObject;
			Card childCard = childGameObject.GetComponent<Card>();
			childCard.interval = GameManager.Instance.CD * childCard.interval;
			childCard.consume = (int)(GameManager.Instance.consume * childCard.consume);
		}
	}
	public void InitUI()
	{
		sumNumberText.text = GameManager.Instance.SunNumber.ToString();
		progressPanel.gameObject.SetActive(false);
	}
	public void UpdateUI()
	{
		sumNumberText.text = GameManager.Instance.SunNumber.ToString();
	}
	public void InitProgressPanel()
	{
		LevelInfoItem levelInfo = GameManager.Instance.levelInfo.LevelInfoList[GameManager.Instance.curLevelId - 1];
		foreach(float position in levelInfo.progressPercent) 
			progressPanel.SetFlagPercent(position);
		progressPanel.SetPercent(0);
		progressPanel.gameObject.SetActive(true);
	}
	public void UpdateProgressPanel()
	{
		int progressNum = 0;
		for(int i = 0; i < GameManager.Instance.levelData.levelDataList.Count; i++)
		{
			LevelItem levelItem = GameManager.Instance.levelData.levelDataList[i];
			if(levelItem.levelId == GameManager.Instance.curLevelId && levelItem.progressId == GameManager.Instance.curProgressId)
				progressNum++;
		}
		int remainNum = GameManager.Instance.curProgressZombie.Count;
		float percent = (float)(progressNum - remainNum) / progressNum;
		LevelInfoItem levelInfoItem = GameManager.Instance.levelInfo.LevelInfoList[GameManager.Instance.curLevelId - 1];
		float progressPercent = levelInfoItem.progressPercent[GameManager.Instance.curProgressId - 1];
		float lastProgressPercent = 0;
		if(GameManager.Instance.curProgressId > 1)
			lastProgressPercent = levelInfoItem.progressPercent[GameManager.Instance.curProgressId - 2];
		float finalPercent = percent * (progressPercent - lastProgressPercent) + lastProgressPercent;
		progressPanel.SetPercent(finalPercent);
	}



}
