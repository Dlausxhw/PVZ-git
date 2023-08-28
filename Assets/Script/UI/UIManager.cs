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
	public AllCardPanel allCardPanel;
	public ChooseCardPanel chooseCardPanel;
	private void Start()
	{
		Instance = this;
	}
	public void InitUI()
	{
		sumNumberText.text = GameManager.Instance.SunNumber.ToString();
		progressPanel.gameObject.SetActive(false);
		allCardPanel.InitCards();
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
