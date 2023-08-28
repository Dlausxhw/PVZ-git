using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlantInfo", fileName = "PlantInfo", order = 1)]
public class PlantInfo : ScriptableObject
{
	public List<PlantInfoItem> PlantInfoList = new List<PlantInfoItem>();
}
[System.Serializable]
public class PlantInfoItem
{
	public int plantId;
	public string plantName;
	public string description;
	// public int consum;
	// public float interval
	public GameObject cardPrefab;
	public override string ToString()
	{
		return "[id]: " + plantId;
	}
}