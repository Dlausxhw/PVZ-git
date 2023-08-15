using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "betaLevelInfo", fileName = "LevelInfo", order = 1)]
public class LevelInfo : ScriptableObject
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