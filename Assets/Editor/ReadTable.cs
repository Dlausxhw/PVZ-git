using Codice.Client.BaseCommands;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Startup
{
	
	public static bool loadFile = false;
	
	static Startup()
	{
		if(!loadFile) return;
		LevelInfo level = new LevelInfo();
		LevelInfoItem item = new LevelInfoItem();
		item.levelId = 0;
		item.LevelName = "¹Ø¿¨1";
		item.progressPercent.Add(0.33f);
		item.progressPercent.Add(0.75f);
		item.progressPercent.Add(1.0f);
		level.LevelInfoList.Add(item);
		Debug.Log("Assets/Resources/LevelInfo.asset");
		AssetDatabase.CreateAsset(level, "Assets/Resources/LevelInfo.asset");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		string path = Application.dataPath + "/Resources/Data/Level1.xlsx";
		Debug.Log(path);
		string assetName = "Level";
		FileInfo fileInfo = new FileInfo(path);
		LevelData levelData = (LevelData)ScriptableObject.CreateInstance(typeof(LevelData));
		
		using(ExcelPackage excelPackage = new ExcelPackage(fileInfo))
		{
			ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Zombie"];
			for(int i = worksheet.Dimension.Start.Row + 2; i <= worksheet.Dimension.End.Row; i++)
			{
				LevelItem levelItem = new LevelItem();
				Type type = typeof(LevelItem);
				for(int j = worksheet.Dimension.Start.Column; j <= worksheet.Dimension.End.Column; j++)
				{
					FieldInfo variable = type.GetField(worksheet.GetValue(2, j).ToString());
					string tableValue = worksheet.GetValue(i, j).ToString();
					variable.SetValue(levelItem, Convert.ChangeType(tableValue, variable.FieldType));
				}
				levelData.levelDataList.Add(levelItem);
			}
		}
		AssetDatabase.CreateAsset(levelData, "Assets/Resources/" + assetName + ".asset");
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
	
}
