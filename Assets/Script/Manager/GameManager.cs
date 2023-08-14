using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public enum PlantType { customize, Peashooter, SunFlower, WallNut, other }
public enum Direction { Left = -1, Right = 1 }
public enum CreateMode { Random, Table }
public static class DirectionExtensions 
{ public static Vector3 vector3(this Direction direction) => new Vector3((int)direction, 0, 0); }


public class GameManager : MonoBehaviour
{
	// Instance
    public static GameManager Instance;
	public GlobalsVariable globals = new GlobalsVariable();

	[Header("Config")]
	// config
	public int SunNumber = 50;
	public float CD = 1.0f;
	public float consume = 1.0f;

	[Header("prefab")]
	// prefab
	public GameObject bornParent;
	public GameObject zombiePrefab;
	public LevelData levelData;
	public LevelInfo levelInfo;

	[Header("current state")]
	// current state
	public bool StartCreateZombie = true;
	private int zOrderIndex = 0;
	public CreateMode createMode = CreateMode.Table;
	public bool gameStart;
	public int curLevelId = 1;
	public int curProgressId = 1;
	public Dictionary<GameObject, int> curProgressZombie;

	private void Awake() => Instance = this;
	private void Start() => ReadTable();
	public void ChangeSunNumber(int changeNumber)
	{
		SunNumber += changeNumber;
		if(SunNumber < 0) SunNumber = 0;
		UIManager.Instance.UpdateUI();
	}
	
	public void ReadTable() => StartCoroutine(LoadTable());
	IEnumerator LoadTable()
	{
		ResourceRequest requestData = Resources.LoadAsync("Level");
		yield return requestData;
		levelData = requestData.asset as LevelData;
		ResourceRequest requestInfo = Resources.LoadAsync("LevelInfo");
		levelInfo = requestInfo.asset as LevelInfo;
		yield return requestInfo;
		GameStart();
	}
	public void GameStart()
	{
		UIManager.Instance.InitUI();
		CreateZombie();
		InvokeRepeating("CreateSunDown", 10, 10);
	}
	public void CreateZombie() {
		curProgressZombie = new Dictionary<GameObject, int>();
		TableCreateZombie();
		UIManager.Instance.InitProgressPanel();
	}
	private void TableCreateZombie()
	{
		bool canCreate = false;
		for(int i = 0; i < levelData.levelDataList.Count; i++)
		{
			LevelItem levelItem = levelData.levelDataList[i];
			if(levelItem.levelId == curLevelId && levelItem.progressId == curProgressId) {
				StartCoroutine(ITableCreateZombie(levelItem));
				canCreate = true;
			}
		}
		if(!canCreate)
		{
			StopAllCoroutines();
			gameStart = false;
			curProgressZombie = new Dictionary<GameObject, int>();
		}
	}
	IEnumerator ITableCreateZombie(LevelItem levelItem)
	{
		yield return new WaitForSeconds(levelItem.createTime);
		GameObject zombiePrefab = Resources.Load("Prefab/Zombie" + levelItem.zombieType.ToString()) as GameObject;
		GameObject zombie = Instantiate(zombiePrefab);
		Transform zombieLine = bornParent.transform.Find("born" + levelItem.bornPos.ToString());
		zombie.transform.parent = zombieLine;
		zombie.transform.localPosition = Vector3.zero;
		zombie.GetComponent<Renderer>().sortingOrder = zOrderIndex++;
		curProgressZombie.Add(zombie, levelItem.bornPos);
		System.Type type = globals.GetType();
		PropertyInfo property = type.GetProperty("Line" + levelItem.bornPos.ToString() + "Zombie");
		if(property != null && property.CanWrite)
			property.SetValue(type, ((int)property.GetValue(type)) + 1);
	}
	public void ZombieDead(GameObject zombie)
	{
		if(curProgressZombie.ContainsKey(zombie))
		{
			System.Type type = globals.GetType();
			PropertyInfo property = type.GetProperty("Line" + curProgressZombie[zombie].ToString() + "Zombie");
			if(property != null && property.CanWrite)
				property.SetValue(type, ((int)property.GetValue(type)) - 1);
			curProgressZombie.Remove(zombie);
			UIManager.Instance.UpdateProgressPanel();
		}
		if(curProgressZombie.Count <= 0)
		{
			curProgressId++;
			TableCreateZombie();
		}
	}
	public void CreateSunDown()
	{
		Vector3 leftBottom = Camera.main.ViewportToWorldPoint(Vector2.zero);
		Vector3 rightTop = Camera.main.ViewportToWorldPoint(Vector2.one);
		GameObject sunPrefab = Resources.Load("Prefab/NormalSun") as GameObject;
		float x = Random.Range(leftBottom.x + 30, rightTop.x - 30);
		Vector3 bornPos = new Vector3(x, rightTop.y, 0);
		GameObject sun = Instantiate(sunPrefab, bornPos, Quaternion.identity);
		float y = Random.Range(leftBottom.y + 100, leftBottom.y + 30);
		sun.GetComponent<NormalSun>().SetTargetPos(new Vector3(bornPos.x, y, 0));
	}
	public class GlobalsVariable
	{
		public int Line0Zombie = 0;
		public int Line1Zombie = 0;
		public int Line2Zombie = 0;
		public int Line3Zombie = 0;
		public int Line4Zombie = 0;
	}
}

