using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData: ScriptableObject
{
	public List<LevelItem> levelDataList = new List<LevelItem>();
}
[System.Serializable]
public class LevelItem
{
	public int id;
	public int levelId;
	public int progressId;
	public int createTime;
	public int zombieType;
	public int bornPos;
	public override string ToString()
	{
		return "[id]: " + id;
	}
}
[CreateAssetMenu(menuName = "betaLevelInfo", fileName = "LevelInfo", order = 1)]
public class LevelInfo: ScriptableObject
{
	public List<LevelInfoItem> LevelInfoList = new List<LevelInfoItem>();
}
[System.Serializable]
public class LevelInfoItem
{
	public int levelId;
	public string LevelName;
	public List<float> progressPercent = new List<float>();
	public override string ToString()
	{
		return "[id]: " + levelId;
	}
}

